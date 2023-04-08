using Pijze.Application.Breweries.Commands;
using Pijze.Application.Common.Commands;
using Pijze.Domain.Entities;
using Pijze.Domain.Repositories;

namespace Pijze.Application.Breweries.Handlers;

internal class AddBreweryHandler : ICommandHandler<AddBrewery>
{
    private readonly IBreweryRepository _breweryRepository;

    public AddBreweryHandler(IBreweryRepository breweryRepository)
    {
        _breweryRepository = breweryRepository;
    }

    public Task HandleAsync(AddBrewery command)
    {
        _breweryRepository.Add(Brewery.Create(Guid.NewGuid(), command.Name));
        return Task.CompletedTask;
    }
}