using Microsoft.Extensions.DependencyInjection;
using Pixogram.Post.Query.Application.Contracts.Handlers;

namespace Pixogram.Post.Query.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IEventHandler, Handlers.EventHandler>();

        return services;
    }
}
