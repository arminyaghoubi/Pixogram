using CQRS.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Pixogram.Post.Query.Application.Queries;

namespace Pixogram.Post.Query.API.Endpoints;

public static class PostEndpoints
{
    public static IEndpointRouteBuilder MapPostEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("/api/v1/post");

        group.MapGet("/", async (
            IQueryDispatcher dispatcher,
            [FromQuery] int page,
            [FromQuery] int pageSize) =>
        {
            FindAllPostsQuery query = new(page, pageSize);
            var result = await dispatcher.SendAsync(query);
            return Results.Ok(result);
        });

        group.MapGet("/{id:guid:required}", async (
            IQueryDispatcher dispatcher,
            [FromRoute] Guid id) =>
        {
            FindPostByIdQuery query = new(id);
            var result = await dispatcher.SendAsync(query);
            return Results.Ok(result);
        });

        group.MapGet("/Author", async (
            IQueryDispatcher dispatcher,
            [FromQuery] string username) =>
        {
            FindPostsByAuthorQuery query = new(username);
            var result = await dispatcher.SendAsync(query);
            return Results.Ok(result);
        });

        return builder;
    }
}
