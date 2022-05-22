using Microsoft.EntityFrameworkCore;
using Pijze.Application.Beers.Dto;
using Pijze.Application.Beers.Queries;
using Pijze.Application.Common.Queries;
using Pijze.Infrastructure.Data;

namespace Pijze.Infrastructure.Queries.Beers;

internal class GetBeersHandler : IQueryHandler<GetBeers, IEnumerable<BeerListItemDto>>
{
    private readonly PijzeDbContext _context;

    public GetBeersHandler(PijzeDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BeerListItemDto>> HandleAsync(GetBeers query)
    {
        return await _context.Beers.OrderByDescending(x=>x.CreationDate).AsNoTracking().Select(beer => new BeerListItemDto(beer.Id, beer.Manufacturer, beer.Name, beer.Rating)).ToListAsync();
    }
}