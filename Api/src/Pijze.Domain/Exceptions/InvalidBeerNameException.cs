using Pijze.Domain.SeedWork;

namespace Pijze.Domain.Exceptions;

public class InvalidBeerNameException : PijzeException
{
    public InvalidBeerNameException() : base("Beer name cannot be empty.")
    {
    }
}