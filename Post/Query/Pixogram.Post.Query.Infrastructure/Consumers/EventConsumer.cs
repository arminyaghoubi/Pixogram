using Confluent.Kafka;
using CQRS.Core.Consumers;
using CQRS.Core.Messages.Events;
using Microsoft.Extensions.Options;
using Pixogram.Post.Query.Application.Contracts.Handlers;
using Pixogram.Post.Query.Infrastructure.Converters;
using System.Text.Json;

namespace Pixogram.Post.Query.Infrastructure.Consumers;

public class EventConsumer(
    IOptions<ConsumerConfig> config,
    IEventHandler eventHandler) : IEventConsumer
{
    private readonly ConsumerConfig _config = config.Value;
    private readonly IEventHandler _eventHandler = eventHandler;

    public void Consume(string topic, CancellationToken cancellation)
    {
        using var consumer = new ConsumerBuilder<string, string>(_config)
            .SetKeyDeserializer(Deserializers.Utf8)
            .SetValueDeserializer(Deserializers.Utf8)
            .Build();

        consumer.Subscribe(topic);

        while (!cancellation.IsCancellationRequested)
        {
            var consumeResult = consumer.Consume();

            if (consumeResult?.Message == null) continue;

            var options = new JsonSerializerOptions { Converters = { new EventJsonConverter() } };
            var @event = JsonSerializer.Deserialize<BaseEvent>(consumeResult.Message.Value, options);

            var handlerMethod = _eventHandler.GetType().GetMethod("On", new Type[] { @event.GetType() });

            if (handlerMethod == null) throw new ArgumentNullException("handler not found!");

            handlerMethod.Invoke(_eventHandler, [@event]);
            consumer.Commit(consumeResult);
        }
    }
}
