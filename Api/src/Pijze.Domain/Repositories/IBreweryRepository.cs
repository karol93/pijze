using Pijze.Domain.Entities;
using Pijze.Domain.SeedWork;

namespace Pijze.Domain.Repositories;

public interface IBreweryRepository : IRepository
{
    Task<bool> Exists(Guid id);
    Task<Brewery?> Find(Guid id);
    void Add(Brewery beer);
    void Delete(Brewery beer);
}