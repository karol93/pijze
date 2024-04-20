using Pijze.Application.Breweries.Commands;
using Pijze.Application.Common.Commands;
using Pijze.Domain.Entities;
using Pijze.Domain.Repositories;

namespace Pijze.Application.Breweries.Handlers;

internal class AddBreweryHandler : ICommandHandler<AddBrewery,Guid>
{
    private readonly IBreweryRepository _breweryRepository;

    public AddBreweryHandler(IBreweryRepository breweryRepository)
    {
        _breweryRepository = breweryRepository;
    }

    public Task<Guid> HandleAsync(AddBrewery command)
    {
        var id = Guid.NewGuid();
        _breweryRepository.Add(Brewery.Create(id, command.Name));
        return Task.FromResult(id);
    }
}