using CQRS.Core.Messages.Events;
namespace CQRS.Core.Infrastructure;

public interface IEventStoreRepository
{
    Task SaveAsync(EventModel @event);
    Task<List<EventModel>> FindByAggreageIdAsync(Guid aggregateId);
}
