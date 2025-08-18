using CQRS.Core.Messages.Commands;

namespace Pixogram.Post.Command.Application.Commands;

public class DeleteCommentCommand : BaseCommand
{
    public string Username { get; set; } = null!;
}
