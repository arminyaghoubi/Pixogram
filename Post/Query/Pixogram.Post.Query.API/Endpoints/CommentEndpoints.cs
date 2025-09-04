using CQRS.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Pixogram.Post.Query.Application.Queries;

namespace Pixogram.Post.Query.API.Endpoints;

public static class CommentEndpoints
{
    public static IEndpointRouteBuilder MapCommentEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("/api/v1/Comment");

        group.MapGet("/", async (
            IQueryDispatcher dispatcher,
            [FromQuery] Guid postId,
            [FromQuery] int page,
            [FromQuery] int pageSize) =>
        {
            FindCommentsByPostIdQuery query = new(postId, page, pageSize);
            var result = await dispatcher.SendAsync(query);
            return Results.Ok(result);
        });

        return group;
    }
}