using Microsoft.EntityFrameworkCore;
using Pijze.Domain.Beers;

namespace Pijze.Infrastructure.Data.EntityFramework.Repositories;

internal class BeerRepository : IBeerRepository
{
    private readonly DbSet<Beer> _beers;

    public BeerRepository(PijzeDbContext context)
    {
        _beers = context.Beers;
    }

    public async Task<Beer?> FindAsync(Guid id) => await _beers.FindAsync(id);
    public void Add(Beer beer) => _beers.Add(beer);

    public void Delete(Beer beer) => _beers.Remove(beer);
    public async Task<IEnumerable<Beer>> GetAllAsync()
    {
        return await _beers.ToListAsync();
    }
}