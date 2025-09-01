using CQRS.Core.DTOs;

namespace Pixogram.Post.Command.API.DTOs;

public record DeletePostResponse(Guid PostId, string? Message) : BaseResponse(Message);
