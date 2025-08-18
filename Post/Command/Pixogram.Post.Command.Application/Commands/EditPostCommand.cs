using CQRS.Core.Messages.Commands;

namespace Pixogram.Post.Command.Application.Commands;

public class EditPostCommand : BaseCommand
{
    public string Message { get; set; } = null!;
}
