using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Pijze.Application.Beers.Commands;
using Pijze.Application.Beers.Exceptions;
using Pijze.Application.Beers.Handlers;
using Pijze.Domain.Services;
using Xunit;

namespace Pijze.Application.Tests.Beers.Handlers;

public class UpdateBeerHandlerTests
{
    [Fact]
    public void ShouldThrowExceptionWhenBeerDoesNotExist()
    {
        // var beerRepository = new Mock<IBeerRepository>();
        // beerRepository.Setup(x => x.FindAsync(It.IsAny<Guid>())).ReturnsAsync(() => null);
        // var handler = new UpdateBeerHandler(beerRepository.Object, new Mock<IImageService>().Object);
        //
        // Func<Task> act = async () => await handler.HandleAsync(new UpdateBeer(Guid.NewGuid(), "Test", "Test", 5, "AAA=="));
        //
        // act.Should().ThrowAsync<BeerNotFoundException>();
    }
   
}