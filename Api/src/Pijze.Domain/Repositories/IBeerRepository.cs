using Pijze.Domain.Entities;
using Pijze.Domain.SeedWork;

namespace Pijze.Domain.Repositories;

public interface IBeerRepository : IRepository
{
    Task<Beer?> Find(AggregateId id);
    void Add(Beer beer);
    void Delete(Beer beer);
    Task<IEnumerable<Beer>> GetAllAsync();
}