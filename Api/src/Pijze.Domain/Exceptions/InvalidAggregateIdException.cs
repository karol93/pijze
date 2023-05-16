using Pijze.Domain.SeedWork;

namespace Pijze.Domain.Exceptions;

public class InvalidAggregateIdException : PijzeException
{
    public Guid Id { get; }

    public InvalidAggregateIdException(Guid id) : base($"Invalid aggregate id: {id}")
        => Id = id;
}