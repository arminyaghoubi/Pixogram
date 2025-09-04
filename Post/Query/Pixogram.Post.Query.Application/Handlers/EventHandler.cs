using Pixogram.Post.Common.Events;
using Pixogram.Post.Query.Infrastructure.Handlers;
using Pixogram.Post.Query.Domain.Contracts.Repositories;
using Pixogram.Post.Query.Domain.Entities;

namespace Pixogram.Post.Query.Application.Handlers;

public class EventHandler : IEventHandler
{
    private readonly IPostRepository _postRepository;
    private readonly ICommentRepository _commentRepository;

    public EventHandler(
        IPostRepository postRepository,
        ICommentRepository commentRepository)
    {
        _postRepository = postRepository;
        _commentRepository = commentRepository;
    }

    public async Task On(CommentAddedEvent @event)
    {
        var comment = new CommentEntity
        {
            Id = @event.CommentId,
            PostId = @event.Id,
            Username = @event.Username,
            Message = @event.Comment,
            CommentDate = @event.CreationDate,
            IsEdited = false,
        };

        await _commentRepository.CreateAsync(comment);
    }

    public async Task On(CommentDeletedEvent @event)
    {
        await _commentRepository.DeleteAsync(@event.CommentId);
    }

    public async Task On(CommentUpdatedEvent @event)
    {
        var comment = await _commentRepository.GetCommentByIdAsync(@event.CommentId);

        if (comment == null) return;

        comment.Message = @event.Comment;
        comment.IsEdited = true;
        comment.CommentDate = @event.ModificationDate;

        await _commentRepository.UpdateAsync(comment);
    }

    public async Task On(PostCreatedEvent @event)
    {
        var post = new PostEntity
        {
            Id = @event.Id,
            Author = @event.Author,
            Message = @event.Message,
            DatePosted = @event.CreationDate,
            Likes = 0
        };

        await _postRepository.CreateAsync(post);
    }

    public async Task On(PostDeletedEvent @event)
    {
        await _postRepository.DeleteAsync(@event.Id);
    }

    public async Task On(PostLikedEvent @event)
    {
        var post = await _postRepository.GetPostByIdAsync(@event.Id);

        if (post == null) return;

        post.Likes++;
        await _postRepository.UpdateAsync(post);
    }

    public async Task On(PostUpdatedEvent @event)
    {
        var post= await _postRepository.GetPostByIdAsync(@event.Id);

        if (post == null) return;

        post.Message=@event.Message;
        await _postRepository.UpdateAsync(post);
    }
}
