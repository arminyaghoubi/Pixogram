using Microsoft.Extensions.Caching.Distributed;
using Pixogram.Post.Query.Application.Contracts.Cache;
using System.Text.Json;

namespace Pixogram.Post.Query.Infrastructure.Cache;

public class RedisService : ICacheService
{
    private readonly IDistributedCache _cache;

    public RedisService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<TValue?> GetValueAsync<TValue>(string key, CancellationToken cancellation = default)
    {
        var json = await _cache.GetStringAsync(key, cancellation);
        if (json == null) return default;

        return JsonSerializer.Deserialize<TValue>(json);
    }

    public async Task RemoveAsync(string key, CancellationToken cancellation = default)
    {
        await _cache.RemoveAsync(key, cancellation);
    }

    public async Task SetAsync<TValue>(string key, TValue value, CancellationToken cancellation = default)
    {
        var json = JsonSerializer.Serialize(value);
        var options = new DistributedCacheEntryOptions
        {
            SlidingExpiration = TimeSpan.FromMinutes(5),
        };

        await _cache.SetStringAsync(key, json, options, cancellation);
    }
}
