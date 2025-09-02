using CQRS.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Pixogram.Post.Command.API.DTOs;
using Pixogram.Post.Command.Application.Commands;

namespace Pixogram.Post.Command.API.Endpoints;

public static class PostEndpoints
{
    public static IEndpointRouteBuilder MapPostEndpoints(this IEndpointRouteBuilder endpoint)
    {
        var group = endpoint.MapGroup("/api/v1/Post");

        group.MapPost("/", async (
            ICommandDispatcher dispatcher,
            NewPostCommand command) =>
        {
            command.Id = Guid.NewGuid();
            await dispatcher.SendAsync(command);
            var response = new NewPostResponse(command.Id, "New post creation request completed successfully.");
            return Results.Created("", response);
        });

        group.MapPut("/", async (
            ICommandDispatcher dispatcher,
            EditPostCommand command) =>
        {
            await dispatcher.SendAsync(command);
            var response = new EditPostResponse(command.Id, "Post modification request completed successfully.");
            return Results.Ok(response);
        });

        group.MapPost("/Like", async (
            ICommandDispatcher dispatcher,
            LikePostCommand command) =>
        {
            await dispatcher.SendAsync(command);
            var response = new LikePostResponse(command.Id, "Like request completed successfully.");
            return Results.Ok(response);
        });

        group.MapDelete("/", async (
            ICommandDispatcher dispatcher,
            [FromBody]DeletePostCommand command) =>
        {
            await dispatcher.SendAsync(command);
            DeletePostResponse response = new(command.Id, "Delete request completed successfully.");
            return Results.Ok(response);
        });

        return endpoint;
    }
}
