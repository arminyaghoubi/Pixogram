using CQRS.Core.Messages.Events;

namespace CQRS.Core.Producers;

public interface IEventProducer
{
    Task ProduceAsync<TEvent>(string topic, TEvent @event) where TEvent : BaseEvent;
}
