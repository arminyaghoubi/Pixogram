using CQRS.Core.Infrastructure;
using CQRS.Core.Messages.Events;
using MongoDB.Driver;

namespace Pixogram.Post.Command.Infrastructure.Repositories;

public class EventStoreRepository(IMongoCollection<EventModel> collection) : IEventStoreRepository
{
    private readonly IMongoCollection<EventModel> _collection = collection;

    public async Task<List<EventModel>> FindByAggreageIdAsync(Guid aggregateId)
    {
        var result = await _collection.FindAsync(e => e.AggregateIdentifier == aggregateId).ConfigureAwait(false);
        return await result.ToListAsync();
    }

    public async Task SaveAsync(EventModel @event)
    {
        await _collection.InsertOneAsync(@event);
    }
}
