using Pijze.Domain.SeedWork;

namespace Pijze.Application.Beers.Exceptions;

public class BeerNotFoundException : PijzeException
{
    public BeerNotFoundException(Guid id) : base($"Beer with id {id} was not found.")
    {
    }
}