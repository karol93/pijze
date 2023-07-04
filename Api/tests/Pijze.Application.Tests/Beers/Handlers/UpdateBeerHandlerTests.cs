using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Pijze.Application.Beers.Commands;
using Pijze.Application.Beers.Exceptions;
using Pijze.Application.Beers.Handlers;
using Pijze.Domain.Entities;
using Pijze.Domain.Repositories;
using Pijze.Domain.SeedWork;
using Pijze.Domain.Services.Interfaces;
using Pijze.Domain.ValueObjects;
using Pijze.Tests.Shared.Builders.Entities;
using Xunit;

namespace Pijze.Application.Tests.Beers.Handlers;

public class UpdateBeerHandlerTests
{
    [Fact]
    public async Task ShouldThrowExceptionWhenBeerDoesNotExist()
    {
        var beerService = new Mock<IBeerService>();
        var beerRepository = new Mock<IBeerRepository>();
        var beerId = AggregateId.Create();
        beerRepository.Setup(x => x.Find(beerId)).ReturnsAsync(() => null);
        var handler =
            new UpdateBeerHandler(beerRepository.Object, new Mock<IImageService>().Object, beerService.Object);

        Func<Task> act = async () =>
            await handler.HandleAsync(new UpdateBeer(Guid.NewGuid(), "Test", Guid.NewGuid(), 5, "AAA=="));

        await act.Should().ThrowAsync<BeerNotFoundException>();
        beerService.Verify(
            x => x.Update(It.IsAny<Beer>(), It.IsAny<string>(), It.IsAny<AggregateId>(), It.IsAny<Rating>(),
                It.IsAny<BeerImage>()), Times.Never);
    }

    [Fact]
    public async Task ShouldCallUpdateMethodOnBeerServiceWhenBeerExists()
    {
        var imageService = new Mock<IImageService>();
        imageService.Setup(x => x.Resize(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>())).Returns(new byte[] { });
        var beerService = new Mock<IBeerService>();
        var beerRepository = new Mock<IBeerRepository>();
        var beerId = AggregateId.Create();
        var beer = new BeerBuilder().Build();
        beerRepository.Setup(x => x.Find(beerId)).ReturnsAsync(beer);
        var handler = new UpdateBeerHandler(beerRepository.Object, imageService.Object, beerService.Object);
        string newName = "Test";
        AggregateId newBreweryId = Guid.NewGuid();
        Rating newRating = 5;
        string newPhoto = "ASDKOASODA==";
        BeerImage newImage = BeerImage.Create(newPhoto, imageService.Object);

        await handler.HandleAsync(new UpdateBeer(beerId, newName, newBreweryId, newRating, newPhoto));

        beerRepository.Verify(x => x.Find(beerId), Times.Once);
        beerService.Verify(x => x.Update(beer, newName, newBreweryId, newRating, newImage), Times.Once);
    }
}