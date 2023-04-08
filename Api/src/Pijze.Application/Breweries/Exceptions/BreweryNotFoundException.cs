using Pijze.Domain.SeedWork;

namespace Pijze.Application.Breweries.Exceptions;

public class BreweryNotFoundException : PijzeException
{
    public BreweryNotFoundException(string message) : base(message)
    {
    }
}