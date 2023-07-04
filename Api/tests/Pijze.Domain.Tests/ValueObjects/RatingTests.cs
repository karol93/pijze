using System;
using FluentAssertions;
using Pijze.Domain.Exceptions;
using Pijze.Domain.ValueObjects;
using Xunit;

namespace Pijze.Domain.Tests.ValueObjects;

public class RatingTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void ShouldCreateValidRating(int validRating)
    {
        Rating rating = Rating.Create(validRating);

        rating.Value.Should().Be(validRating);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(6)]
    public void ShouldThrowExceptionWhenCreatingRatingWithInvalidValue(int invalidRating)
    {
        Action action = () => Rating.Create(invalidRating);
    
        action.Should().Throw<InvalidRatingException>();
    }
    
    [Fact]
    public void ShouldConvertRatingToIntImplicitly()
    {
        int validRating = 4;
        Rating rating = Rating.Create(validRating);
    
        int convertedRating = rating;
    
        convertedRating.Should().Be(validRating);
    }
    
    [Fact]
    public void ShouldConvertIntToRatingImplicitly()
    {
        int validRating = 2;
    
        Rating rating = validRating;
    
        rating.Value.Should().Be(validRating);
    }
    
    [Fact]
    public void ShouldThrowExceptionWhenCreatingRatingWithInvalidValueUsingImplicitConversion()
    {
        int invalidRating = 6;

        Action action = () =>
        {
            Rating rating = invalidRating;
        };
    
        action.Should().Throw<InvalidRatingException>();
    }
    
    [Fact]
    public void ShouldThrowExceptionWhenCreatingRatingWithInvalidValueUsingExplicitConversion()
    {
        int invalidRating = 6;

        Action action = () =>
        {
            Rating rating = invalidRating;
        };
    
        action.Should().Throw<InvalidRatingException>();
    }
}