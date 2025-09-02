using CQRS.Core.DTOs;

namespace Pixogram.Post.Command.API.DTOs;

public record EditCommentResponse(Guid CommentId, string? Message) : BaseResponse(Message);
