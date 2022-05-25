using Pijze.Application.Beers.Dto;
using Pijze.Application.Beers.Queries;
using Pijze.Application.Common.Queries;
using Pijze.Infrastructure.Data;

namespace Pijze.Infrastructure.Queries.Beers;

internal class FindBeerHandler : IQueryHandler<FindBeer, BeerDto?>
{
    private readonly PijzeDbContext _context;

    public FindBeerHandler(PijzeDbContext context)
    {
        _context = context;
    }

    public async Task<BeerDto?> HandleAsync(FindBeer query)
    {
        var beer = await _context.Beers.FindAsync(query.Id);
        return beer is null
            ? null
            : new BeerDto(beer.Id, beer.Manufacturer, beer.Name, beer.Rating,  beer.Image.ToBase64());
    }
}