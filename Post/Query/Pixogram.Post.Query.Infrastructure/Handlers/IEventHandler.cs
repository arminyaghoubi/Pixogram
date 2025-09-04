using Pixogram.Post.Common.Events;

namespace Pixogram.Post.Query.Infrastructure.Handlers;

public interface IEventHandler
{
    Task On(CommentAddedEvent @event);
    Task On(CommentDeletedEvent @event);
    Task On(CommentUpdatedEvent @event);
    Task On(PostCreatedEvent @event);
    Task On(PostDeletedEvent @event);
    Task On(PostLikedEvent @event);
    Task On(PostUpdatedEvent @event);
}
