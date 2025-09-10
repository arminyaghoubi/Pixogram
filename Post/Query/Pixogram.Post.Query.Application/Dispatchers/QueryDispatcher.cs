using CQRS.Core.Infrastructure;
using CQRS.Core.Messages.Queries;

namespace Pixogram.Post.Query.Application.Dispatchers;

public class QueryDispatcher : IQueryDispatcher
{
    private readonly Dictionary<Type,Func<dynamic,Task<dynamic>>> _handlers=new();

    public void RegisterHandler<TQuery, TResult>(Func<TQuery, Task<TResult>> handler) where TQuery : BaseQuery<TResult>
    {
        if(_handlers.ContainsKey(typeof(TQuery)))
            throw new IndexOutOfRangeException("Query Handler Duplicate.");

        _handlers.Add(typeof(TQuery),async (x)=> await handler(x));
    }

    public async Task<TResult> SendAsync<TResult>(BaseQuery<TResult> query)
    {
        if(!_handlers.TryGetValue(query.GetType(),out var handler))
            throw new ArgumentNullException("Query Handler Not Found.");

        return await handler(query);
    }
}
