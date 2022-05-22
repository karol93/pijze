namespace Pijze.Application.Beers.Exceptions;

public class BeerNotFoundException : Exception
{
    public BeerNotFoundException(string message) : base(message)
    {
    }
}