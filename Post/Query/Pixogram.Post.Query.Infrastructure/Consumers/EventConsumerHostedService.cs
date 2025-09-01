using CQRS.Core.Consumers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Pixogram.Post.Query.Infrastructure.Consumers;

public class EventConsumerHostedService(
    ILogger<EventConsumerHostedService> logger,
    IServiceProvider provider) : IHostedService
{
    private readonly ILogger<EventConsumerHostedService> _logger = logger;
    private readonly IServiceProvider _provider = provider;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Event Consumer Started...");

        var topic = Environment.GetEnvironmentVariable("KAFKA_TOPIC") ?? throw new ArgumentNullException("KAFKA_TOPIC Not Found!");
        Task.Run(() =>
        {
            using var scope = _provider.CreateScope();
            var consumer = scope.ServiceProvider.GetRequiredService<IEventConsumer>();
            consumer.Consume(topic, cancellationToken);
        });

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Event Consumer Stoped...");

        return Task.CompletedTask;
    }
}
