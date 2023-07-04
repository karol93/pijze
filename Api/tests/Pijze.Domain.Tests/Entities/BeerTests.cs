using System;
using FluentAssertions;
using Pijze.Domain.Entities;
using Pijze.Domain.Exceptions;
using Pijze.Domain.SeedWork;
using Pijze.Domain.ValueObjects;
using Pijze.Tests.Shared.Builders.Entities;
using Xunit;

namespace Pijze.Domain.Tests.Entities;

public class BeerTests
{
    [Fact]
    public void ShouldCreateBeer()
    {
        AggregateId id = AggregateId.Create();
        AggregateId breweryId = AggregateId.Create();
        string name = "Test Beer";
        Rating rating = 5;
        
        Beer beer = Beer.Create(id, name, breweryId, rating, new BeerImageBuilder().Build());
        
        beer.Should().NotBeNull();
        beer.Id.Should().Be(id);
        beer.Name.Should().Be(name);
        beer.BreweryId.Should().Be(breweryId);
        beer.Rating.Should().Be(rating);
        beer.Image.Should().NotBeNull();
    }
    
    [Fact]
    public void ShouldThrowExceptionWhenCreatingBeerWithEmptyName()
    {
        AggregateId breweryId = AggregateId.Create();
        int rating = 5;
        
        Action action = () => Beer.Create(Guid.NewGuid(), string.Empty, breweryId, rating, new BeerImageBuilder().Build());
        
        action.Should().Throw<InvalidBeerNameException>();
    }

    [Fact]
    public void ShouldThrowExceptionWhenCreatingBeerWithNullName()
    {
        AggregateId breweryId = AggregateId.Create();
        int rating = 5;

        Action action = () => Beer.Create(Guid.NewGuid(), null!, breweryId, rating, new BeerImageBuilder().Build());

        action.Should().Throw<InvalidBeerNameException>();
    }
    
    [Fact]
    public void ShouldThrowExceptionWhenUpdatingBeerWithEmptyName()
    {
        var existingBeer = CreateBeer();
        
        Action action = () => existingBeer.Update(string.Empty, existingBeer.BreweryId, existingBeer.Rating, existingBeer.Image);
        
        action.Should().Throw<InvalidBeerNameException>();
    }
    
    [Fact]
    public void ShouldThrowExceptionWhenUpdatingBeerWithNullName()
    {
        var existingBeer = CreateBeer();

        Action action = () => existingBeer.Update(null!, existingBeer.BreweryId, existingBeer.Rating, existingBeer.Image);

        action.Should().Throw<InvalidBeerNameException>();
    }

    [Fact]
    public void ShouldUpdateBeer()
    {
        var beer = CreateBeer();
        string newName = "New Name";
        AggregateId newBreweryId = AggregateId.Create();
        Rating newRating = 3;
        var newBeerImage=  new BeerImageBuilder().WithBytes(new byte[]{0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40}).Build();
        
        beer.Update(newName, newBreweryId, newRating, newBeerImage);
        
        beer.Name.Should().Be(newName);
        beer.BreweryId.Should().Be(newBreweryId);
        beer.Rating.Should().Be(newRating);
        beer.Image.Should().BeEquivalentTo(newBeerImage);
    }

    private static Beer CreateBeer()
    {
        AggregateId id = AggregateId.Create();
        string name = "Test Beer";
        AggregateId breweryId = AggregateId.Create();
        Rating rating = 5;
        var beerImage = new BeerImageBuilder().WithBytes(new byte[] {0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20}).Build();
        Beer beer = Beer.Create(id, name, breweryId, rating, beerImage);
        return beer;
    }
}