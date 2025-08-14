using CQRS.Core.Messages.Events;

namespace CQRS.Core.Application;

public interface IStoreEventService
{
    Task<List<BaseEvent>> GetEventsAsync(Guid aggregateId);
    Task SaveEventAsync(Guid aggregateId, IEnumerable<BaseEvent> events, int expectVersion);
}
