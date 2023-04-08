using Pijze.Application.Breweries.Commands;
using Pijze.Application.Breweries.Exceptions;
using Pijze.Application.Common.Commands;
using Pijze.Domain.Repositories;

namespace Pijze.Application.Breweries.Handlers;

internal class UpdateBreweryHandler : ICommandHandler<UpdateBrewery>
{
    private readonly IBreweryRepository _breweryRepository;

    public UpdateBreweryHandler(IBreweryRepository breweryRepository)
    {
        _breweryRepository = breweryRepository;
    }

    public async Task HandleAsync(UpdateBrewery command)
    {
        var brewery = await _breweryRepository.Find(command.Id);

        if (brewery == null) throw new BreweryNotFoundException($"Beer with id {command.Id} was not found.");
        
        brewery.Update(command.Name);
    }
}