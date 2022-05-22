namespace Pijze.Application.Common.Caching;

public interface ICacheKey<TItem>
{
    string CacheKey { get; }
}