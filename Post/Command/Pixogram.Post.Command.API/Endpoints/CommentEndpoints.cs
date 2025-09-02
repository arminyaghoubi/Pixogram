using CQRS.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Pixogram.Post.Command.API.DTOs;
using Pixogram.Post.Command.Application.Commands;

namespace Pixogram.Post.Command.API.Endpoints;

public static class CommentEndpoints
{
    public static void MapCommentEndpoints(
        this IEndpointRouteBuilder endpoint)
    {
        var group = endpoint.MapGroup("/api/v1/Comment");

        group.MapPost("/", async (
            ICommandDispatcher dispatcher,
            AddCommentCommand command) =>
        {
            await dispatcher.SendAsync(command);
            AddCommentResponse response = new(command.Id, "Add request completed successfully.");
            return Results.Created("", response);
        });

        group.MapPut("/", async (
            ICommandDispatcher dispatcher,
            EditCommentCommand command) =>
        {
            await dispatcher.SendAsync(command);
            EditCommentResponse response = new(command.Id, "Edit request completed successfully.");
            return Results.Ok(response);
        });

        group.MapDelete("/", async (
            ICommandDispatcher dispatcher,
            [FromBody]DeleteCommentCommand command) =>
        {
            await dispatcher.SendAsync(command);
            EditCommentResponse response = new(command.Id, "Delete request completed successfully.");
            return Results.Ok(response);
        });
    }
}