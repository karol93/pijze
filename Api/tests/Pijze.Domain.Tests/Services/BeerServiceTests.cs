using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Pijze.Domain.Entities;
using Pijze.Domain.Exceptions;
using Pijze.Domain.Repositories;
using Pijze.Domain.SeedWork;
using Pijze.Domain.Services;
using Pijze.Domain.Services.Interfaces;
using Pijze.Domain.ValueObjects;
using Pijze.Tests.Shared.Builders.Entities;
using Xunit;
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace Pijze.Domain.Tests.Services;

public class BeerServiceTests
{
    private readonly Mock<IBeerRepository> _beerRepository;
    private readonly Mock<IBreweryRepository> _breweryRepository;
    
    public BeerServiceTests()
    {
        _beerRepository = new Mock<IBeerRepository>();
        _breweryRepository = new Mock<IBreweryRepository>();
    }
    
    [Fact]
    public async Task ShouldCreateBeerWhenBreweryExists()
    {
        var beerService = new BeerService(_breweryRepository.Object,_beerRepository.Object);
        var breweryId = AggregateId.Create();
        _breweryRepository.Setup(x => x.Exists(breweryId)).ReturnsAsync(true);
        
        await beerService.Create(AggregateId.Create(), "beer", breweryId, Rating.Create(3),
            BeerImage.Create("ASDKOASODA==", new Mock<IImageService>().Object));

        _beerRepository.Verify(x => x.Add(It.IsAny<Beer>()), Times.Once);
    }

    [Fact]
    public async Task ShouldThrowsExceptionWhenCreatingBeerAndBreweryDoesNotExist()
    {
        var beerService = new BeerService(_breweryRepository.Object,_beerRepository.Object);
        var breweryId = AggregateId.Create();
        _breweryRepository.Setup(x => x.Exists(breweryId)).ReturnsAsync(false);
        
        Func<Task> action = async () => await beerService.Create(AggregateId.Create(), "beer", breweryId, Rating.Create(3),
            BeerImage.Create("ASDKOASODA==", new Mock<IImageService>().Object));

        await action.Should().ThrowAsync<MissingBreweryForBeerException>();
        _beerRepository.Verify(x => x.Add(It.IsAny<Beer>()), Times.Never);
    }

    
    [Fact]
    public async Task ShouldThrowsExceptionWhenUpdatingBeerAndBreweryDoesNotExist()
    {
        Beer beer = new BeerBuilder().Build();
        var beerService = new BeerService(_breweryRepository.Object,_beerRepository.Object);
        var newBreweryId = AggregateId.Create();
        _breweryRepository.Setup(x => x.Exists(newBreweryId)).ReturnsAsync(false);
        string newName = "new beer name";
        Rating newRating = Rating.Create(4);
        BeerImage newImage = BeerImage.Create("ASDKOASODA==", new Mock<IImageService>().Object);
        
        Func<Task> action = async () => await beerService.Update(beer, newName, newBreweryId, newRating, newImage);

        await action.Should().ThrowAsync<MissingBreweryForBeerException>();
    }
    
}