using Pijze.Application.Beers.CacheKeys;
using Pijze.Application.Common.Caching;
using Pijze.Application.Common.Queries;

namespace Pijze.Application.Beers.Queries;

public record GetBeerImage(Guid BeerId) : ICacheableQuery<byte[]?>
{
    public ICacheKey<byte[]?> CacheKey => new BeerImageCacheKey(BeerId);
}