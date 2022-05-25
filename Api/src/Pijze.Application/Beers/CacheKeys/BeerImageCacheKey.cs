using Pijze.Application.Common.Caching;

namespace Pijze.Application.Beers.CacheKeys;

internal class BeerImageCacheKey: ICacheKey<byte[]?>
{
    private readonly Guid _beerId;

    public BeerImageCacheKey(Guid beerId)
    {
        _beerId = beerId;
    }
    
    public string CacheKey => $"BeerImage_{_beerId}";
}