using Pijze.Application.Beers.Commands;
using Pijze.Application.Common.Commands;
using Pijze.Domain.Beers;

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
        var beer = await _beerRepository.FindAsync(command.Id);
        if(beer == null) return;
        _beerRepository.Delete(beer);
    }
}