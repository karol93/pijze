using Pijze.Domain.Entities;
using Pijze.Domain.Exceptions;
using Pijze.Domain.Repositories;
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

    public async Task Create(Guid id, string name, Guid breweryId, Rating rating, BeerImage image)
    {
        await ValidateBrewery(breweryId);
        
        var beer = Beer.Create(Guid.NewGuid(), name, breweryId, rating, image);
        _beerRepository.Add(beer);
    }


    public async Task Update(Beer beer, string name, Guid breweryId, Rating rating, BeerImage image)
    {
        await ValidateBrewery(breweryId);
        beer.Update(name, breweryId, rating, image);
    }
    
    private async Task ValidateBrewery(Guid breweryId)
    {
        if (!await _breweryRepository.Exists(breweryId))
            throw new MissingBreweryForBeerException("Given brewery does not exist.");
    }
}