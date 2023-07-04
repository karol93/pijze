using Pijze.Domain.SeedWork;

namespace Pijze.Domain.Exceptions;

public class InvalidBase64FormatException : PijzeException
{
    public InvalidBase64FormatException() : base("Invalid base64 format")
    {
    }
}