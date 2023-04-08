using Pijze.Application.Beers.Commands;
using Pijze.Application.Beers.Exceptions;
using Pijze.Application.Common.Commands;
using Pijze.Domain.Repositories;

namespace Pijze.Application.Beers.Handlers;

internal class DeleteBeerHandler : ICommandHandler<DeleteBeer>
{
    private readonly IBeerRepository _beerRepository;
    
    public DeleteBeerHandler(IBeerRepository beerRepository)
    {
        _beerRepository = beerRepository;
    }

    public async Task HandleAsync(DeleteBeer command)
    {
        var beer = await _beerRepository.Find(command.Id);
        if(beer == null) throw new BeerNotFoundException($"Beer with id {command.Id} was not found.");;
        _beerRepository.Delete(beer);
    }
}