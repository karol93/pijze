using Pijze.Domain.SeedWork;

namespace Pijze.Domain.Exceptions;

public class InvalidRatingException : PijzeException
{
    public InvalidRatingException(string message) : base(message)
    {
    }
}