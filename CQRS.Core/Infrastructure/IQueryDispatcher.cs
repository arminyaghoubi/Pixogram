using CQRS.Core.Messages.Queries;

namespace CQRS.Core.Infrastructure;

public interface IQueryDispatcher
{
    void RegisterHandler<TQuery, TResult>(Func<TQuery, Task<TResult>> handler) where TQuery : BaseQuery<TResult>;
    Task<TResult> SendAsync<TResult>(BaseQuery<TResult> query);
}
