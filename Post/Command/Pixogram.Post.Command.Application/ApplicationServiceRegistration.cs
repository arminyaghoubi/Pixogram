using CQRS.Core.Application;
using CQRS.Core.Handlers;
using CQRS.Core.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Pixogram.Post.Command.Application.Commands;
using Pixogram.Post.Command.Application.Commands.Common;
using Pixogram.Post.Command.Application.Handlers;
using Pixogram.Post.Command.Application.Services;
using Pixogram.Post.Command.Domain.Aggregates;
using Pixogram.Post.Command.Infrastructure.Dispatchers;

namespace Pixogram.Post.Command.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IStoreEventService, StoreEventService>();
        services.AddScoped<IEventSourcingHandler<PostAggregate>, EventSourcingHandler>();
        services.AddScoped<ICommandHandler, CommandHandler>();
        services.AddSingleton(_ =>
        {
            var handler = services.BuildServiceProvider().GetRequiredService<ICommandHandler>();
            return GetCommandDispatcher(handler);
        });

        return services;
    }

    private static ICommandDispatcher GetCommandDispatcher(ICommandHandler handler)
    {
        CommandDispatcher dispatcher = new();

        dispatcher.RegisterHandler<AddCommentCommand>(handler.HandleAsync);
        dispatcher.RegisterHandler<DeleteCommentCommand>(handler.HandleAsync);
        dispatcher.RegisterHandler<DeletePostCommand>(handler.HandleAsync);
        dispatcher.RegisterHandler<EditCommentCommand>(handler.HandleAsync);
        dispatcher.RegisterHandler<EditPostCommand>(handler.HandleAsync);
        dispatcher.RegisterHandler<LikePostCommand>(handler.HandleAsync);
        dispatcher.RegisterHandler<NewPostCommand>(handler.HandleAsync);

        return dispatcher;
    }
}
