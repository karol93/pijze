using Pijze.Application.Breweries.Commands;
using Pijze.Application.Breweries.Exceptions;
using Pijze.Application.Common.Commands;
using Pijze.Domain.Repositories;

namespace Pijze.Application.Beers.Handlers;

internal class DeleteBreweryHandler : ICommandHandler<DeleteBrewery>
{
    private readonly IBreweryRepository _breweryRepository;

    public DeleteBreweryHandler(IBreweryRepository breweryRepository)
    {
        _breweryRepository = breweryRepository;
    }

    public async Task HandleAsync(DeleteBrewery command)
    {
        // todo: check beers assigned to this brewery
        var brewery = await _breweryRepository.Find(command.Id);
        if(brewery == null) throw new BreweryNotFoundException($"Brewery with id {command.Id} was not found.");;
        _breweryRepository.Delete(brewery);
    }
}