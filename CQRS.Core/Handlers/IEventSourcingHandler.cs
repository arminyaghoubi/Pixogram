using CQRS.Core.Domain;

namespace CQRS.Core.Handlers;

public interface IEventSourcingHandler<TAggregate>
{
    Task<TAggregate> GetByIdAsync(Guid aggregateId);
    Task SaveAsync(AggregateRoot aggregateRoot);
}
