using CQRS.Core.Messages.Events;

namespace CQRS.Core.Domain;

public abstract class AggregateRoot
{
    protected Guid _id;
    private readonly List<BaseEvent> _uncommitedChanges = new();

    public Guid Id => _id;

    public int Version { get; set; } = -1;

    public IEnumerable<BaseEvent> GetUncommittedChanges() => _uncommitedChanges;

    public void MarkChangesAsCommitted() => _uncommitedChanges.Clear();

    private void ApplyChanges(BaseEvent @event, bool isNew)
    {
        var method = GetType().GetMethod("Apply", new Type[] { @event.GetType() }) ??
            throw new ArgumentNullException($"The Apply method was not found in the aggregate for {@event.GetType().Name}"); ;


        method.Invoke(this, new object[] { @event });

        if (isNew)
            _uncommitedChanges.Add(@event);
    }

    protected void RaiseEvent(BaseEvent @event)
    {
        ApplyChanges(@event, true);
    }

    public void ReplyEvents(IEnumerable<BaseEvent> events)
    {
        foreach (var @event in events)
        {
            ApplyChanges(@event, false);
        }
    }
}
