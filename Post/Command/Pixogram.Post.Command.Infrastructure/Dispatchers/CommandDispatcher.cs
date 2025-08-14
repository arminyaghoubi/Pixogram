using CQRS.Core.Infrastructure;
using CQRS.Core.Messages.Commands;

namespace Pixogram.Post.Command.Infrastructure.Dispatchers;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly Dictionary<Type, Func<BaseCommand, Task>> handlers = new();

    public void RegisterHandler<TCommand>(Func<TCommand, Task> handler) where TCommand : BaseCommand
    {
        if (handlers.ContainsKey(typeof(TCommand)))
            throw new IndexOutOfRangeException("Command Handler Duplicate.");

        handlers.Add(typeof(TCommand), x=> handler((TCommand)x));
    }

    public async Task SendAsync(BaseCommand command)
    {
        if (handlers.TryGetValue(command.GetType(),out var handler))
        {
            await handler(command);
        }
        else
        {
            throw new ArgumentNullException("Command Handler Not Found.");
        }
    }
}
