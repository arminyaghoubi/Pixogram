using CQRS.Core.Application;
using CQRS.Core.Exceptions;
using CQRS.Core.Infrastructure;
using CQRS.Core.Messages.Events;
using CQRS.Core.Producers;
using Pixogram.Post.Command.Domain.Aggregates;

namespace Pixogram.Post.Command.Application.Services;

public class StoreEventService : IStoreEventService
{
    private readonly IEventStoreRepository _eventStoreRepository;
    private readonly IEventProducer _eventProducer;

    public StoreEventService(
        IEventStoreRepository eventStoreRepository,
        IEventProducer eventProducer)
    {
        _eventStoreRepository = eventStoreRepository;
        _eventProducer = eventProducer;
    }

    public async Task<List<BaseEvent>> GetEventsAsync(Guid aggregateId)
    {
        var eventStream = await _eventStoreRepository.FindByAggreageIdAsync(aggregateId);

        if (eventStream == null || eventStream.Count == 0)
            throw new AggregateNotFoundException($"Invalid Post ID = {aggregateId}.");

        return eventStream
            .OrderBy(e => e.Version)
            .Select(e => e.EventData)
            .ToList();
    }

    public async Task SaveEventAsync(Guid aggregateId, IEnumerable<BaseEvent> events, int expectVersion)
    {
        var eventStream = await _eventStoreRepository.FindByAggreageIdAsync(aggregateId);

        if (expectVersion != -1 && eventStream[^1].Version == expectVersion)
            throw new ConcurrencyException();

        var version = expectVersion;

        foreach (var @event in events)
        {
            @event.Version = ++version;
            var eventType = @event.GetType().Name;
            var eventModel = new EventModel
            {
                AggregateIdentifier = aggregateId,
                Version = version,
                EventType = eventType,
                AggregateType = nameof(PostAggregate),
                EventData = @event,
                TimeStamp = DateTime.Now,
            };

            await _eventStoreRepository.SaveAsync(eventModel);

            var topic = Environment.GetEnvironmentVariable("KAFKA_TOPIC") ?? throw new ArgumentNullException("KAFKA_TOPIC Not Found!");
            await _eventProducer.ProduceAsync(topic, @event);
        }
    }
}
