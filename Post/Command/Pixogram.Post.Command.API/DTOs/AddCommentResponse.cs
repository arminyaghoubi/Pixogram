using CQRS.Core.DTOs;

namespace Pixogram.Post.Command.API.DTOs;

public record AddCommentResponse(Guid CommentId, string? Message) : BaseResponse(Message);
