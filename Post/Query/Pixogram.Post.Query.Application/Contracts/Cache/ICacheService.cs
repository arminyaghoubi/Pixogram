namespace Pixogram.Post.Query.Application.Contracts.Cache;

public interface ICacheService
{
    Task<TValue?> GetValueAsync<TValue>(string key, CancellationToken cancellation = default);
    Task RemoveAsync(string key, CancellationToken cancellation = default);
    Task SetAsync<TValue>(string key, TValue value, CancellationToken cancellation = default);
}
