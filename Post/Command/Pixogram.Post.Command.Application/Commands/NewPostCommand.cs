using CQRS.Core.Messages.Commands;

namespace Pixogram.Post.Command.Application.Commands;

public class NewPostCommand : BaseCommand
{
    public string Username { get; set; } = null!;
    public string Message { get; set; } = null!;
}