using CQRS.Core.Infrastructure;
using CQRS.Core.Messages.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Pixogram.Post.Command.Infrastructure.Configurations;
using Pixogram.Post.Command.Infrastructure.Repositories;

namespace Pixogram.Post.Command.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<MongoDbConfiguration>(configuration.GetSection(MongoDbConfiguration.SectionName));
        services.AddScoped<IMongoCollection<EventModel>>(p =>
        {
            using var scope = p.CreateScope();
            var mongoConfig = scope.ServiceProvider.GetService<MongoDbConfiguration>() ?? throw new ArgumentNullException("MongoDbConfig Not Found.");

            var mongoClient = new MongoClient(mongoConfig.ConnectionString);
            var database = mongoClient.GetDatabase(mongoConfig.Database);
            var collection = database.GetCollection<EventModel>(mongoConfig.Collection);

            return collection;
        });

        services.AddScoped<IEventStoreRepository, EventStoreRepository>();

        return services;
    }
}
