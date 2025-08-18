using CQRS.Core.Messages.Commands;

namespace Pixogram.Post.Command.Application.Commands;

public class EditCommentCommand : BaseCommand
{
    public Guid CommentId { get; set; }
    public string Username { get; set; } = null!;
    public string Message { get; set; } = null!;
}
