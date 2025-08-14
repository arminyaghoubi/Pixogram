namespace CQRS.Core.Messages.Events;

public abstract class BaseEvent:Message
{
    protected BaseEvent()
    {
        Type = this.GetType().Name;
    }

    public int Version { get; set; }
    public string Type { get; set; }
}
