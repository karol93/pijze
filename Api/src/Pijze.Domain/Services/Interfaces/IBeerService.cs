using Pijze.Domain.Entities;
using Pijze.Domain.ValueObjects;

namespace Pijze.Domain.Services.Interfaces;

public interface IBeerService
{
    Task Create(Guid id, string name, Guid breweryId, Rating rating, BeerImage image);
    Task Update(Beer beer, string name, Guid breweryId, Rating rating, BeerImage image);
}