using CQRS.Core.DTOs;

namespace Pixogram.Post.Command.API.DTOs;

public record DeleteCommentResponse(Guid CommentId, string? Message) : BaseResponse(Message);