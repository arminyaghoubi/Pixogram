using CQRS.Core.DTOs;

namespace Pixogram.Post.Command.API.DTOs;

public record LikePostResponse(Guid PostId, string? Message) : BaseResponse(Message);
