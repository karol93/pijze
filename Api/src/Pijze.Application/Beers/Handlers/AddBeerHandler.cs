using Pijze.Application.Beers.Commands;
using Pijze.Application.Beers.Dto;
using Pijze.Application.Common.Commands;
using Pijze.Domain.Entities;
using Pijze.Domain.Services.Interfaces;
using Pijze.Domain.ValueObjects;

namespace Pijze.Application.Beers.Handlers;

internal class AddBeerHandler : ICommandHandler<AddBeer,Guid>
{
    private readonly IBeerService _beerService;
    private readonly IImageService _imageService;

    public AddBeerHandler(IBeerService beerService, IImageService imageService)
    {
        _beerService = beerService;
        _imageService = imageService;
    }

    public async Task<Guid> HandleAsync(AddBeer command)
    {
        var id = Guid.NewGuid();
        await _beerService.Create(id, command.Name, command.BreweryId, command.Rating,
            BeerImage.Create(command.Photo, _imageService));
        return id;
    }
}