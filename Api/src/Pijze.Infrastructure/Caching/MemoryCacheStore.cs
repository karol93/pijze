using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using Pijze.Application.Common.Caching;

namespace Pijze.Infrastructure.Caching;

internal class MemoryCacheStore : ICacheStore
{
    private readonly IMemoryCache _memoryCache;
    private CancellationTokenSource _resetCacheToken = new();

    public MemoryCacheStore(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public void Add<TItem>(TItem item, ICacheKey<TItem> key)
    {
        if (item == null) return;
        _memoryCache.Set(key.CacheKey, item, new CancellationChangeToken(_resetCacheToken.Token));
    }

    public TItem? Get<TItem>(ICacheKey<TItem> key) where TItem : class => _memoryCache.TryGetValue(key.CacheKey, out TItem value) ? value : null;

    public void Remove<TItem>(ICacheKey<TItem> key) => _memoryCache.Remove(key.CacheKey);
    
    public void RemoveAll()
    {
        _resetCacheToken.Cancel();
        _resetCacheToken.Dispose();
        _resetCacheToken = new CancellationTokenSource();
    }
}