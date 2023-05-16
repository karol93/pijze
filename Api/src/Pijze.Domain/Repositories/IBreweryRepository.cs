using Pijze.Domain.Entities;
using Pijze.Domain.SeedWork;

namespace Pijze.Domain.Repositories;

public interface IBreweryRepository : IRepository
{
    Task<bool> Exists(AggregateId id);
    Task<Brewery?> Find(AggregateId id);
    void Add(Brewery beer);
    void Delete(Brewery beer);
}