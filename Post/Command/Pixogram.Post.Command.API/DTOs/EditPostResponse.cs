using CQRS.Core.DTOs;

namespace Pixogram.Post.Command.API.DTOs;

public record EditPostResponse(Guid PostId, string? Message) : BaseResponse(Message);
