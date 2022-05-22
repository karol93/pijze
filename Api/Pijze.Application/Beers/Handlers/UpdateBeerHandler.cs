using Pijze.Application.Beers.Commands;
using Pijze.Application.Beers.Exceptions;
using Pijze.Application.Common.Commands;
using Pijze.Domain.Beers;
using Pijze.Domain.Services;

namespace Pijze.Application.Beers.Handlers;

internal class UpdateBeerHandler : ICommandHandler<UpdateBeer>
{
    private readonly IBeerRepository _beerRepository;
    private readonly IImageService _imageService;

    public UpdateBeerHandler(IBeerRepository beerRepository, IImageService imageService)
    {
        _beerRepository = beerRepository;
        _imageService = imageService;
    }

    public async Task HandleAsync(UpdateBeer command)
    {
        var beer = await _beerRepository.FindAsync(command.Id);

        if (beer == null) throw new BeerNotFoundException($"Beer with id {command.Id} was not found.");

        beer.Update(command.Name, command.Manufacturer, command.Rating, BeerImage.Create(command.Photo, _imageService));
    }
}