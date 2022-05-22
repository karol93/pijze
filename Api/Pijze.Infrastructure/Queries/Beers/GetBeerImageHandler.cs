using Microsoft.EntityFrameworkCore;
using Pijze.Application.Beers.Queries;
using Pijze.Application.Common.Queries;
using Pijze.Infrastructure.Data;

namespace Pijze.Infrastructure.Queries.Beers;

internal class GetBeerImageHandler : IQueryHandler<GetBeerImage, byte[]?>
{
    private readonly PijzeDbContext _context;

    public GetBeerImageHandler(PijzeDbContext context)
    {
        _context = context;
    }

    public async Task<byte[]?> HandleAsync(GetBeerImage query)
    {
        var image = await _context.Beers.Where(x => x.Id == query.BeerId).AsNoTracking().Select(x => x.Image).FirstOrDefaultAsync();
        return image?.Bytes;
    }
}
