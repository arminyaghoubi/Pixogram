using Microsoft.Extensions.DependencyInjection;
using Pixogram.Post.Query.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pixogram.Post.Query.Domain.Contracts.Repositories;
using Pixogram.Post.Query.Infrastructure.Repositories;

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

        return services;
    }
}
