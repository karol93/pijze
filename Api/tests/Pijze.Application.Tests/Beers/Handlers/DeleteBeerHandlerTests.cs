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
using Pijze.Tests.Shared.Builders.Entities;
using Xunit;

namespace Pijze.Application.Tests.Beers.Handlers;

public class DeleteBeerHandlerTests
{
    
    [Fact]
    public void ShouldThrowExceptionWhenBeerDoesNotExist()
    {
        var repository = new Mock<IBeerRepository>();
        var beerId = AggregateId.Create();
        repository.Setup(x => x.Find(beerId)).ReturnsAsync((Beer?) null);
        var handler = new DeleteBeerHandler(repository.Object);
        
        Func<Task> act = async () => await handler.HandleAsync(new DeleteBeer(beerId));

        act.Should().ThrowAsync<BeerNotFoundException>();
        repository.Verify(x => x.Delete(It.IsAny<Beer>()), Times.Never);

    }
    
    [Fact]
    public async Task ShouldCallDeleteMethodOnRepositoryWhenBeerExists()
    {
        var repository = new Mock<IBeerRepository>();
        var beerId = AggregateId.Create();
        var beer = new BeerBuilder().Build();
        repository.Setup(x => x.Find(beerId)).ReturnsAsync(beer);
        var handler = new DeleteBeerHandler(repository.Object);

        await handler.HandleAsync(new DeleteBeer(beerId));

        repository.Verify(x => x.Find(beerId), Times.Once);
        repository.Verify(x => x.Delete(beer), Times.Once);
    }
}