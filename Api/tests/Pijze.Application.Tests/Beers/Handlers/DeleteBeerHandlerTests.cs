using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Pijze.Application.Beers.Commands;
using Pijze.Application.Beers.Exceptions;
using Pijze.Application.Beers.Handlers;
using Pijze.Domain.Entities;
using Pijze.Domain.Repositories;
using Pijze.Tests.Shared.Builders.Beers;
using Xunit;

namespace Pijze.Application.Tests.Beers.Handlers;

public class DeleteBeerHandlerTests
{
    
    [Fact]
    public void ShouldThrowExceptionWhenBeerDoesNotExist()
    {
        var repository = new Mock<IBeerRepository>();
        repository.Setup(x => x.Find(It.IsAny<Guid>())).ReturnsAsync((Beer?) null);
        var handler = new DeleteBeerHandler(repository.Object);
        
        Func<Task> act = async () => await handler.HandleAsync(new DeleteBeer(Guid.NewGuid()));

        act.Should().ThrowAsync<BeerNotFoundException>();
    }
    
    [Fact]
    public async Task ShouldCallDeleteMethodOnRepositoryWhenBeerExists()
    {
        var repository = new Mock<IBeerRepository>();
        repository.Setup(x => x.Find(It.IsAny<Guid>())).ReturnsAsync(new BeerBuilder().Build());
        var handler = new DeleteBeerHandler(repository.Object);

        await handler.HandleAsync(new DeleteBeer(Guid.NewGuid()));

        repository.Verify(x => x.Find(It.IsAny<Guid>()), Times.Once);
        repository.Verify(x => x.Delete(It.IsAny<Beer>()), Times.Once);
    }
}