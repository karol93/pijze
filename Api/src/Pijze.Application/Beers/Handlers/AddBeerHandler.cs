using Pijze.Application.Beers.Commands;
using Pijze.Application.Common.Commands;
using Pijze.Domain.Entities;
using Pijze.Domain.Services.Interfaces;
using Pijze.Domain.ValueObjects;

namespace Pijze.Application.Beers.Handlers;

internal class AddBeerHandler : ICommandHandler<AddBeer>
{
    private readonly IBeerService _beerService;
    private readonly IImageService _imageService;

    public AddBeerHandler(IBeerService beerService, IImageService imageService)
    {
        _beerService = beerService;
        _imageService = imageService;
    }

    public async Task HandleAsync(AddBeer command)
    {
        await _beerService.Create(Guid.NewGuid(), command.Name, command.BreweryId, command.Rating,
            BeerImage.Create(command.Photo, _imageService));
    }
}