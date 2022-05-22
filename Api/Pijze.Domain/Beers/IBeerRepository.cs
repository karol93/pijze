using Pijze.Domain.SeedWork;

namespace Pijze.Domain.Beers;

public interface IBeerRepository : IRepository
{
    Task<Beer?> FindAsync(Guid id);
    void Add(Beer beer);
    void Delete(Beer beer);
    Task<IEnumerable<Beer>> GetAllAsync();
}