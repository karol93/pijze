using Pijze.Domain.Exceptions;

namespace Pijze.Domain.ValueObjects;

public record Rating
{
    private const int MinValue = 1;
    private const int MaxValue = 5;
    
    public Rating(int value)
    {
        if (value is < MinValue or > MaxValue)
        {
            throw new InvalidRatingException(
                $"Rating must be greater than or equal to {MinValue} and lower than or equal to {MaxValue}");
        }
        
        Value = value;
    }
    
    public int Value { get; }
    
    public static implicit operator int(Rating rating) => rating;

    public static implicit operator Rating(int rating) => new(rating);
}