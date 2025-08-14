namespace CQRS.Core.Messages.Events;

public record EventModel
{
    public string Id { get; set; } = null!;
    public DateTime TimeStamp { get; set; }
    public Guid AggregateIdentifier { get; set; }
    public string AggregateType { get; set; } = null!;
    public int Version { get; set; }
    public string EventType { get; set; } = null!;
    public BaseEvent EventData { get; set; } = null!;
}
