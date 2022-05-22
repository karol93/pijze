using Pijze.Application.Beers.Commands;
using Pijze.Application.Common.Commands;
using Pijze.Domain.Beers;
using Pijze.Domain.Services;

namespace Pijze.Application.Beers.Handlers;

internal class AddBeerHandler : ICommandHandler<AddBeer>
{
    private readonly IBeerRepository _beerRepository;
    private readonly IImageService _imageService;

    public AddBeerHandler(IBeerRepository beerRepository, IImageService imageService)
    {
        _beerRepository = beerRepository;
        _imageService = imageService;
    }

    public Task HandleAsync(AddBeer command)
    {
        _beerRepository.Add(Beer.Create(Guid.NewGuid(), command.Name, command.Manufacturer, command.Rating,
            BeerImage.Create(command.Photo, _imageService)));
        return Task.CompletedTask;
    }
}