using Pijze.Application.Beers.Dto;
using Pijze.Application.Common.Caching;

namespace Pijze.Application.Beers.CacheKeys;

internal class BeerCacheKey : ICacheKey<BeerDto?>
{
    private readonly Guid _beerId;

    public BeerCacheKey(Guid beerId)
    {
        _beerId = beerId;
    }
    public string CacheKey => $"Beer_{_beerId}";
}