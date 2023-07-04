using Pijze.Application.Beers.Commands;
using Pijze.Application.Beers.Exceptions;
using Pijze.Application.Common.Commands;
using Pijze.Domain.Entities;
using Pijze.Domain.Repositories;
using Pijze.Domain.Services.Interfaces;
using Pijze.Domain.ValueObjects;

namespace Pijze.Application.Beers.Handlers;

internal class UpdateBeerHandler : ICommandHandler<UpdateBeer>
{
    private readonly IBeerRepository _beerRepository;
    private readonly IImageService _imageService;
    private readonly IBeerService _beerService;

    public UpdateBeerHandler(IBeerRepository beerRepository, IImageService imageService, IBeerService beerService)
    {
        _beerRepository = beerRepository;
        _imageService = imageService;
        _beerService = beerService;
    }

    public async Task HandleAsync(UpdateBeer command)
    {
        var beer = await _beerRepository.Find(command.Id);

        if (beer == null) throw new BeerNotFoundException(command.Id);

        await _beerService.Update(beer, command.Name, command.BreweryId, command.Rating,
            BeerImage.Create(command.Photo, _imageService));
    }
}