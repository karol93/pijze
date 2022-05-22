using Pijze.Application.Beers.CacheKeys;
using Pijze.Application.Beers.Dto;
using Pijze.Application.Common.Caching;
using Pijze.Application.Common.Queries;

namespace Pijze.Application.Beers.Queries;

public record GetBeers : ICacheableQuery<IEnumerable<BeerListItemDto>>
{
    public ICacheKey<IEnumerable<BeerListItemDto>> CacheKey => new BeersCacheKey();
}