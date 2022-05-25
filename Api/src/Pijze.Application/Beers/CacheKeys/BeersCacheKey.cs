using Pijze.Application.Beers.Dto;
using Pijze.Application.Common.Caching;

namespace Pijze.Application.Beers.CacheKeys;

internal class BeersCacheKey : ICacheKey<IEnumerable<BeerListItemDto>>
{
    public string CacheKey => "Beers";
}