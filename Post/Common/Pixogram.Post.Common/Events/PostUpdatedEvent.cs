using CQRS.Core.Messages.Events;

namespace Pixogram.Post.Common.Events;

public class PostUpdatedEvent : BaseEvent
{
    public string Message { get; set; }
}
