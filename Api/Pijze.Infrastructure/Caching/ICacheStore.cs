using Pijze.Application.Common.Caching;

namespace Pijze.Infrastructure.Caching;

public interface ICacheStore
{
    void Add<TItem>(TItem item, ICacheKey<TItem> key);
    TItem? Get<TItem>(ICacheKey<TItem> key) where TItem : class;
    void Remove<TItem>(ICacheKey<TItem> key);
    void RemoveAll();
}