using CQRS.Core.Messages.Commands;

namespace CQRS.Core.Infrastructure;

public interface ICommandDispatcher
{
    void RegisterHandler<TCommand>(Func<TCommand, Task> handler) where TCommand : BaseCommand;
    Task SendAsync(BaseCommand command);
}
