using Pijze.Domain.SeedWork;

namespace Pijze.Application.Beers.Exceptions;

public class BeerNotFoundException : PijzeException
{
    public BeerNotFoundException(string message) : base(message)
    {
    }
}