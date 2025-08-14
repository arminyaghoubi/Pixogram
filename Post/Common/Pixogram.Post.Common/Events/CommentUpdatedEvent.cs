using CQRS.Core.Messages.Events;

namespace Pixogram.Post.Common.Events;

public class CommentUpdatedEvent : BaseEvent
{
    public Guid CommentId { get; set; }
    public string Comment { get; set; }
    public string Username { get; set; }
    public DateTime ModificationDate { get; set; }
}
