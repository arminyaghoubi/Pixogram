using CQRS.Core.Messages.Events;

namespace Pixogram.Post.Common.Events;

public class PostCreatedEvent : BaseEvent
{
    public string Author { get; set; }
    public string Message { get; set; }
    public DateTime CreationDate { get; set; }
}