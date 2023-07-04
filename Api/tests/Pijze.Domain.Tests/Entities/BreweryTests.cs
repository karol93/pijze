using System;
using FluentAssertions;
using Pijze.Domain.Entities;
using Pijze.Domain.Exceptions;
using Pijze.Domain.SeedWork;
using Xunit;

namespace Pijze.Domain.Tests.Entities;

public class BreweryTests
{
    [Fact]
    public void ShouldCreateBrewery()
    {
        AggregateId breweryId = AggregateId.Create();
        string name = "Test Brewery";

        Brewery brewery = Brewery.Create(breweryId, name);

        brewery.Should().NotBeNull();
        brewery.Id.Should().Be(breweryId);
        brewery.Name.Should().Be(name);
    }

    [Fact]
    public void ShouldThrowExceptionWhenCreatingBreweryWithEmptyName()
    {
        Action action = () => Brewery.Create(AggregateId.Create(), string.Empty);

        action.Should().Throw<InvalidBreweryNameException>();
    }

    [Fact]
    public void ShouldThrowExceptionWhenCreatingBreweryWithNullName()
    {
        Action action = () => Brewery.Create(AggregateId.Create(), null!);

        action.Should().Throw<InvalidBreweryNameException>();
    }

    [Fact]
    public void ShouldUpdateBrewery()
    {
        var existingBrewery = Brewery.Create(AggregateId.Create(), "OldName");
        string newName = "New Brewery Name";

        existingBrewery.Update(newName);

        existingBrewery.Name.Should().Be(newName);
    }

    [Fact]
    public void ShouldThrowExceptionWhenUpdatingBreweryWithEmptyName()
    {
        var existingBrewery = Brewery.Create(AggregateId.Create(), "OldName");

        Action action = () => existingBrewery.Update(string.Empty);

        action.Should().Throw<InvalidBreweryNameException>();
    }

    [Fact]
    public void ShouldThrowExceptionWhenUpdatingBreweryWithNullName()
    {
        var existingBrewery = Brewery.Create(AggregateId.Create(), "OldName");

        Action action = () => existingBrewery.Update(null!);

        action.Should().Throw<InvalidBreweryNameException>();
    }
}