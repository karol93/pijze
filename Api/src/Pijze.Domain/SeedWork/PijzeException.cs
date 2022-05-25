namespace Pijze.Domain.SeedWork;

public abstract class PijzeException : Exception
{
    protected PijzeException(string message) : base(message)
    {
    }
}