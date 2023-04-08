using Microsoft.EntityFrameworkCore;
using Pijze.Domain.Entities;
using Pijze.Domain.Repositories;

namespace Pijze.Infrastructure.Data.EntityFramework.Repositories;

internal class BreweryRepository : IBreweryRepository
{
    private readonly DbSet<Brewery> _breweries;

    public BreweryRepository(PijzeDbContext context)
    {
        _breweries = context.Breweries;
    }

    public async Task<bool> Exists(Guid id) => await _breweries.AnyAsync(x => x.Id == id);

    public async Task<Brewery?> Find(Guid id) => await _breweries.FindAsync(id);

    public void Add(Brewery brewery) => _breweries.Add(brewery);

    public void Delete(Brewery brewery) => _breweries.Remove(brewery);
}