using Pijze.Domain.SeedWork;

namespace Pijze.Domain.Exceptions;

public class InvalidBreweryNameException : PijzeException
{
public InvalidBreweryNameException() : base("Brewery name cannot be empty.")
{
}
}