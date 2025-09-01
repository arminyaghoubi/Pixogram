using Confluent.Kafka;
using CQRS.Core.Infrastructure;
using CQRS.Core.Messages.Events;
using CQRS.Core.Producers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Pixogram.Post.Command.Infrastructure.Configurations;
using Pixogram.Post.Command.Infrastructure.Producers;
using Pixogram.Post.Command.Infrastructure.Repositories;
using Pixogram.Post.Common.Events;

namespace Pixogram.Post.Command.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        BsonClassMap.RegisterClassMap<BaseEvent>();
        BsonClassMap.RegisterClassMap<PostCreatedEvent>();
        BsonClassMap.RegisterClassMap<PostUpdatedEvent>();
        BsonClassMap.RegisterClassMap<PostDeletedEvent>();
        BsonClassMap.RegisterClassMap<PostLikedEvent>();
        BsonClassMap.RegisterClassMap<CommentAddedEvent>();
        BsonClassMap.RegisterClassMap<CommentUpdatedEvent>();
        BsonClassMap.RegisterClassMap<CommentDeletedEvent>();

        services.Configure<ProducerConfig>(configuration.GetSection(nameof(ProducerConfig)));
        services.Configure<MongoDbConfiguration>(configuration.GetSection(MongoDbConfiguration.SectionName));
        services.AddScoped<IMongoCollection<EventModel>>(p =>
        {
            using var scope = p.CreateScope();
            var mongoConfig = scope.ServiceProvider.GetService<IOptions<MongoDbConfiguration>>()?.Value ?? throw new ArgumentNullException("MongoDbConfig Not Found.");

            var mongoClient = new MongoClient(mongoConfig.ConnectionString);
            var database = mongoClient.GetDatabase(mongoConfig.Database);
            var collection = database.GetCollection<EventModel>(mongoConfig.Collection);

            return collection;
        });

        services.AddScoped<IEventStoreRepository, EventStoreRepository>();
        services.AddScoped<IEventProducer, EventProducer>();

        return services;
    }
}
