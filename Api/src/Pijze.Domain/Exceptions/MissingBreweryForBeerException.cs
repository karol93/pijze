using Pijze.Domain.SeedWork;

namespace Pijze.Domain.Exceptions;

public class MissingBreweryForBeerException : PijzeException
{
    public MissingBreweryForBeerException(string message) : base(message){}
}