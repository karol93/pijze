using Pijze.Domain.Entities;
using Pijze.Domain.Exceptions;
using Pijze.Domain.Repositories;
using Pijze.Domain.SeedWork;
using Pijze.Domain.Services.Interfaces;
using Pijze.Domain.ValueObjects;

namespace Pijze.Domain.Services;

internal class BeerService : IBeerService
{
    private readonly IBeerRepository _beerRepository;
    private readonly IBreweryRepository _breweryRepository;

    public BeerService(IBreweryRepository breweryRepository, IBeerRepository beerRepository)
    {
        _breweryRepository = breweryRepository;
        _beerRepository = beerRepository;
    }

    public async Task Create(AggregateId id, string name, AggregateId breweryId, Rating rating, BeerImage image)
    {
        await ValidateBrewery(breweryId);
        
        var beer = Beer.Create(id, name, breweryId, rating, image);
        _beerRepository.Add(beer);
    }


    public async Task Update(Beer beer, string name, AggregateId breweryId, Rating rating, BeerImage image)
    {
        await ValidateBrewery(breweryId);
        beer.Update(name, breweryId, rating, image);
    }
    
    private async Task ValidateBrewery(AggregateId breweryId)
    {
        if (!await _breweryRepository.Exists(breweryId))
            throw new MissingBreweryForBeerException("Given brewery does not exist.");
    }
}