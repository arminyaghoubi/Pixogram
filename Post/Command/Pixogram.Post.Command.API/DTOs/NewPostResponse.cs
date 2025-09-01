using CQRS.Core.DTOs;

namespace Pixogram.Post.Command.API.DTOs;

public record NewPostResponse(Guid PostId, string? Message) : BaseResponse(Message);
