using Confluent.Kafka;
using CQRS.Core.Messages.Events;
using CQRS.Core.Producers;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Pixogram.Post.Command.Infrastructure.Producers;

public class EventProducer : IEventProducer
{
    private readonly ProducerConfig _config;

    public EventProducer(IOptions<ProducerConfig> options)
    {
        _config = options.Value;
    }

    public async Task ProduceAsync<TEvent>(string topic, TEvent @event) where TEvent : BaseEvent
    {
        using var producer = new ProducerBuilder<string, string>(_config).Build();

        var message = new Message<string, string>
        {
            Key = Guid.NewGuid().ToString(),
            Value = JsonSerializer.Serialize(@event,@event.GetType())
        };

        var result= await producer.ProduceAsync(topic,message);

        if (result.Status == PersistenceStatus.NotPersisted)
            throw new Exception($"Could not produce {@event.GetType().Name} message to topic {topic}.({result.Message})");
    }
}
