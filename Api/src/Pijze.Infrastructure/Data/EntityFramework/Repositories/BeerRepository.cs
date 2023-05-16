using Microsoft.EntityFrameworkCore;
using Pijze.Domain.Entities;
using Pijze.Domain.Repositories;
using Pijze.Domain.SeedWork;

namespace Pijze.Infrastructure.Data.EntityFramework.Repositories;

internal class BeerRepository : IBeerRepository
{
    private readonly DbSet<Beer> _beers;

    public BeerRepository(PijzeDbContext context)
    {
        _beers = context.Beers;
    }

    public async Task<Beer?> Find(AggregateId id) => await _beers.FindAsync(id);
    public void Add(Beer beer) => _beers.Add(beer);

    public void Delete(Beer beer) => _beers.Remove(beer);
    public async Task<IEnumerable<Beer>> GetAllAsync()
    {
        return await _beers.ToListAsync();
    }
}