using CQRS.Core.Messages.Commands;

namespace Pixogram.Post.Command.Application.Commands;

public class DeletePostCommand : BaseCommand
{
    public string Username { get; set; } = null!;
}
