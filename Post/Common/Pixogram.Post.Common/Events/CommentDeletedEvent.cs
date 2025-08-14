using CQRS.Core.Messages.Events;

namespace Pixogram.Post.Common.Events;

public class CommentDeletedEvent : BaseEvent
{
    public Guid CommentId { get; set; }
}
