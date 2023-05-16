using Pijze.Domain.Entities;
using Pijze.Domain.SeedWork;
using Pijze.Domain.ValueObjects;

namespace Pijze.Domain.Services.Interfaces;

public interface IBeerService
{
    Task Create(AggregateId id, string name, AggregateId breweryId, Rating rating, BeerImage image);
    Task Update(Beer beer, string name, AggregateId breweryId, Rating rating, BeerImage image);
}