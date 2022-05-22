using Pijze.Application.Common.Caching;

namespace Pijze.Application.Common.Queries;

public interface ICacheableQuery<T> : IQuery<T>
{
    public ICacheKey<T> CacheKey { get; }
}