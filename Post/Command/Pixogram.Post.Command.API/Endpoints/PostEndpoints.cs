using CQRS.Core.Infrastructure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Pixogram.Post.Command.Application.Commands;

namespace Pixogram.Post.Command.API.Endpoints;

public static class PostEndpoints
{
    public static void MapPostEndpoints(this IEndpointRouteBuilder endpoint)
    {
        var group = endpoint.MapGroup("/api/v1/Post");

        group.MapPost("/", async (
            ILogger<NewPostCommand> logger,
            ICommandDispatcher dispatcher,
            NewPostCommand command) =>
        {
            command.Id = Guid.NewGuid();
            await dispatcher.SendAsync(command);
            return Results.Created();
        });
    }
}
