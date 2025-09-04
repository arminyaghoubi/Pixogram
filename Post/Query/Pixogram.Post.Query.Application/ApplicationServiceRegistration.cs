using CQRS.Core.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Pixogram.Post.Query.Application.Contracts.Handlers;
using Pixogram.Post.Query.Application.Handlers;
using Pixogram.Post.Query.Application.Queries;
using Pixogram.Post.Query.Domain.Entities;
using Pixogram.Post.Query.Infrastructure.Dispatchers;
using Pixogram.Post.Query.Infrastructure.Handlers;

namespace Pixogram.Post.Query.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IEventHandler, Handlers.EventHandler>();
        services.AddScoped<IQueryHandler, QueryHandler>();
        services.AddSingleton<IQueryDispatcher>(provider =>
        {
            var handler = services.BuildServiceProvider().GetRequiredService<IQueryHandler>();
            return GetQueryDispatcher(handler);
        });

        return services;
    }

    private static IQueryDispatcher GetQueryDispatcher(IQueryHandler queryHandler)
    {
        QueryDispatcher dispatcher = new();

        dispatcher.RegisterHandler<FindAllPostsQuery, IEnumerable<PostEntity>>(queryHandler.HandlerAsync);
        dispatcher.RegisterHandler<FindPostByIdQuery, PostEntity>(queryHandler.HandlerAsync);
        dispatcher.RegisterHandler<FindPostsByAuthorQuery, IEnumerable<PostEntity>>(queryHandler.HandlerAsync);

        return dispatcher;
    }
}
