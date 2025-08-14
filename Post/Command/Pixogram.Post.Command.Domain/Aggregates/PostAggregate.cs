using CQRS.Core.Domain;
using Pixogram.Post.Common.Events;

namespace Pixogram.Post.Command.Domain.Aggregates;

public class PostAggregate : AggregateRoot
{
    private bool _active;
    private string _author;
    private readonly Dictionary<Guid, Tuple<string, string>> _comments = new();

    public bool Active { get { return _active; } set { _active = value; } }

    public PostAggregate()
    {

    }

    public PostAggregate(Guid id, string author, string message)
    {
        RaiseEvent(new PostCreatedEvent
        {
            Id = id,
            Author = author,
            Message = message,
            CreationDate = DateTime.Now,
        });
    }

    public void Apply(PostCreatedEvent @event)
    {
        _id = @event.Id;
        _author = @event.Author;
        _active = true;
    }

    public void EditMessage(string newMessage)
    {
        if (!_active)
            throw new InvalidOperationException("You cannot edit the message.");

        if (string.IsNullOrEmpty(newMessage))
            throw new InvalidOperationException("Invalid message value.");

        RaiseEvent(new PostUpdatedEvent
        {
            Id = _id,
            Message = newMessage,
        });
    }

    public void Apply(PostUpdatedEvent @event)
    {
        _id = @event.Id;
    }

    public void LikePost()
    {
        if (!_active)
            throw new InvalidOperationException("You cannot like this post.");

        RaiseEvent(new PostLikedEvent
        {
            Id = _id,
        });
    }

    public void Apply(PostLikedEvent @event)
    {
        _id = @event.Id;
    }

    public void AddComment(string username, string comment)
    {
        if (!_active)
            throw new InvalidOperationException("You cannot send comment for this post.");

        if (string.IsNullOrEmpty(comment))
            throw new InvalidOperationException("Invalid comment value.");

        RaiseEvent(new CommentAddedEvent
        {
            Id = _id,
            Username = username,
            Comment = comment,
            CreationDate = DateTime.Now,
            CommentId = Guid.NewGuid()
        });
    }

    public void Apply(CommentAddedEvent @event)
    {
        _id = @event.Id;
        _comments.Add(@event.CommentId, new Tuple<string, string>(@event.Username, @event.Comment));
    }

    public void EditComment(Guid commentId, string username, string comment)
    {
        if (!_active)
            throw new InvalidOperationException("You cannot update comment for this post.");

        if (string.IsNullOrEmpty(comment))
            throw new InvalidOperationException("Invalid comment value.");

        if (!_comments.TryGetValue(commentId, out var currentComment))
            throw new DirectoryNotFoundException("Comment Not Found.");

        if (currentComment.Item1 != username)
            throw new InvalidOperationException("Yow are not allowed to edit a comment that was made by another user!");

        RaiseEvent(new CommentUpdatedEvent
        {
            Id = _id,
            CommentId = commentId,
            Username = username,
            Comment = comment,
            ModificationDate = DateTime.Now,
        });
    }

    public void Apply(CommentUpdatedEvent @event)
    {
        _id = @event.Id;
        _comments[@event.CommentId] = new Tuple<string, string>(@event.Username, @event.Comment);
    }

    public void RemoveComment(Guid commentId, string username)
    {
        if (!_active)
            throw new InvalidOperationException("You cannot rmeove comment for this post.");

        if (!_comments.TryGetValue(commentId, out var currentComment))
            throw new DirectoryNotFoundException("Comment Not Found.");

        if (currentComment.Item1 != username)
            throw new InvalidOperationException("Yow are not allowed to edit a comment that was made by another user!");

        RaiseEvent(new CommentDeletedEvent
        {
            Id = _id,
            CommentId = commentId,
        });
    }

    public void Apply(CommentDeletedEvent @event)
    {
        _id = @event.Id;
        _comments.Remove(@event.CommentId);
    }

    public void DeletePost(string username)
    {
        if (!_active)
            throw new InvalidOperationException("The post has already been removed!");

        if (_author != username)
            throw new InvalidOperationException("You are not allowed to delete a post that was made by someone else!");

        RaiseEvent(new PostDeletedEvent
        {
            Id = _id,
        });
    }

    public void Apply(PostDeletedEvent @event)
    {
        _id = @event.Id;
        _active = false;
    }
}
