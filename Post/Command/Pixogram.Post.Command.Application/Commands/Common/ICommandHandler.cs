namespace Pixogram.Post.Command.Application.Commands.Common;

public interface ICommandHandler
{
    Task HandleAsync(AddCommentCommand command);
    Task HandleAsync(DeleteCommentCommand command);
    Task HandleAsync(DeletePostCommand command);
    Task HandleAsync(EditCommentCommand command);
    Task HandleAsync(EditPostCommand command);
    Task HandleAsync(LikePostCommand command);
    Task HandleAsync(NewPostCommand command);
}
