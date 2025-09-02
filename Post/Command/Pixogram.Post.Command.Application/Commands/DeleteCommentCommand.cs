using CQRS.Core.Messages.Commands;

namespace Pixogram.Post.Command.Application.Commands;

public class DeleteCommentCommand : BaseCommand
{
    public Guid CommentId { get; set; }
    public string Username { get; set; } = null!;
}
