using System;
using FluentAssertions;
using Pijze.Domain.Beers;
using Pijze.Tests.Shared.Builders.Beers;
using Xunit;

namespace Pijze.Domain.Tests.Beers;

public class BeerTests
{
    [Fact]
    public void ShouldCreateBeer()
    {
        Guid id = Guid.NewGuid();
        string name = "Test Beer";
        string manufacturer = "Test Manufacturer";
        int rating = 5;


        Beer beer = Beer.Create(id, name, manufacturer, rating, new BeerImageBuilder().Build());

        
        beer.Should().NotBeNull();
        beer.Id.Should().Be(id);
        beer.Name.Should().Be(name);
        beer.Manufacturer.Should().Be(manufacturer);
        beer.Rating.Should().Be(rating);
        beer.Image.Should().NotBeNull();
    }
    
    [Fact]
    public void ShouldThrowExceptionWhenCreatingBeerWithEmptyName()
    {
        string manufacturer = "Test Manufacturer";
        int rating = 5;

        Action action = () => Beer.Create(Guid.NewGuid(), string.Empty, manufacturer, rating, new BeerImageBuilder().Build());

        action.Should().Throw<ArgumentNullException>();
    }
    
    [Fact]
    public void ShouldThrowExceptionWhenCreatingBeerWithEmptyManufacturer()
    {
        string name = "Test Beer";
        int rating = 5;

        Action action = () => Beer.Create(Guid.NewGuid(), name, string.Empty, rating, new BeerImageBuilder().Build());

        action.Should().Throw<ArgumentNullException>();
    }
    
    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void ShouldThrowExceptionWhenCreatingBeerWithRatingLessThanOne(int rating)
    {
        string name = "Test Beer";
        string manufacturer = "Test Manufacturer";

        Action action = () => Beer.Create(Guid.NewGuid(), name, manufacturer, rating, new BeerImageBuilder().Build());

        action.Should().Throw<ArgumentOutOfRangeException>();
    }
    
    [Fact]
    public void ShouldThrowExceptionWhenCreatingBeerWithRatingGreaterThanFive()
    {
        string name = "Test Beer";
        string manufacturer = "Test Manufacturer";

        Action action = () => Beer.Create(Guid.NewGuid(), name, manufacturer, 6, new BeerImageBuilder().Build());

        action.Should().Throw<ArgumentOutOfRangeException>();
    }
    
    [Fact]
    public void ShouldUpdateBeer()
    {
        Guid id = Guid.NewGuid();
        string name = "Test Beer";
        string manufacturer = "Test Manufacturer";
        int rating = 5;
        var beerImage=  new BeerImageBuilder().WithBytes(new byte[]{0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20}).Build();
        Beer beer = Beer.Create(id, name, manufacturer, rating, beerImage);
        string newName = "New Name";
        string newManufacturer = "New Manufacturer";
        int newRating = 3;
        var newBeerImage=  new BeerImageBuilder().WithBytes(new byte[]{0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40}).Build();


        beer.Update(newName, newManufacturer, newRating, newBeerImage);

        
        beer.Name.Should().Be(newName);
        beer.Manufacturer.Should().Be(newManufacturer);
        beer.Rating.Should().Be(newRating);
        beer.Image.Should().BeEquivalentTo(newBeerImage);
    }
    
}