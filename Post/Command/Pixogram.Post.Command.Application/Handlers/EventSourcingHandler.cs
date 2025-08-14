using CQRS.Core.Application;
using CQRS.Core.Domain;
using CQRS.Core.Handlers;
using Pixogram.Post.Command.Domain.Aggregates;

namespace Pixogram.Post.Command.Application.Handlers;

public class EventSourcingHandler : IEventSourcingHandler<PostAggregate>
{
    private readonly IStoreEventService _storeEventService;

    public EventSourcingHandler(IStoreEventService storeEventService)
    {
        _storeEventService = storeEventService;
    }

    public async Task<PostAggregate> GetByIdAsync(Guid aggregateId)
    {
        PostAggregate aggregate = new();

        var events = await _storeEventService.GetEventsAsync(aggregateId);
        if (events == null || !events.Any())
            return aggregate;

        aggregate.ReplyEvents(events);
        aggregate.Version = events.Max(e => e.Version);

        return aggregate;
    }

    public async Task SaveAsync(AggregateRoot aggregate)
    {
        await _storeEventService.SaveEventAsync(
            aggregate.Id,
            aggregate.GetUncommittedChanges(),
            aggregate.Version);

        aggregate.MarkChangesAsCommitted();
    }
}
