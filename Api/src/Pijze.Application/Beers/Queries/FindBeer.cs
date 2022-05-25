using Pijze.Application.Beers.CacheKeys;
using Pijze.Application.Beers.Dto;
using Pijze.Application.Common.Caching;
using Pijze.Application.Common.Queries;

namespace Pijze.Application.Beers.Queries;

public record FindBeer(Guid Id) : ICacheableQuery<BeerDto?>
{
    public ICacheKey<BeerDto?> CacheKey => new BeerCacheKey(Id);
}