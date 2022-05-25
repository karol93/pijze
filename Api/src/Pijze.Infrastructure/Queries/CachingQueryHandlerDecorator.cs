using Pijze.Application.Common.Caching;
using Pijze.Application.Common.Queries;
using Pijze.Infrastructure.Caching;

namespace Pijze.Infrastructure.Queries;

internal class CachingQueryHandlerDecorator<TQuery, TResponse> : IQueryHandler<TQuery, TResponse> where TQuery : class, IQuery<TResponse> where TResponse : class
{
    private readonly IQueryHandler<TQuery, TResponse> _handler;
    private readonly ICacheStore _cache;

    public CachingQueryHandlerDecorator(IQueryHandler<TQuery, TResponse> handler, ICacheStore cache)
    {
        _handler = handler;
        _cache = cache;
    }

    public async Task<TResponse> HandleAsync(TQuery query)
    {
        if (query is not ICacheableQuery<TResponse> cacheableQuery) return await _handler.HandleAsync(query);
        
        var cachedResponse = _cache.Get(cacheableQuery.CacheKey);
        if (cachedResponse != null)
            return cachedResponse;
        
        var response = await _handler.HandleAsync(query);
        _cache.Add(response, cacheableQuery.CacheKey);
        return response;
    }
}