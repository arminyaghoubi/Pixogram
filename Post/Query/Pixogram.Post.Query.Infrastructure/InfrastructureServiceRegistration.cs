using Microsoft.Extensions.DependencyInjection;
using Pixogram.Post.Query.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pixogram.Post.Query.Domain.Contracts.Repositories;
using Pixogram.Post.Query.Infrastructure.Repositories;
using Confluent.Kafka;
using Pixogram.Post.Query.Infrastructure.Consumers;
using CQRS.Core.Consumers;
using Pixogram.Post.Query.Infrastructure.Cache;
using Pixogram.Post.Query.Application.Contracts.Cache;

namespace Pixogram.Post.Query.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Pixogram")));
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.Configure<ConsumerConfig>(configuration.GetSection(nameof(ConsumerConfig)));
        services.AddScoped<IEventConsumer, EventConsumer>();
        services.AddHostedService<EventConsumerHostedService>();
        services.AddScoped<ICacheService, RedisService>();
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
            options.InstanceName = "Pixogram_";
        });

        return services;
    }
}
