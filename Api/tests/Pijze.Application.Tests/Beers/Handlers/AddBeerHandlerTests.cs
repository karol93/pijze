using System;
using System.Threading.Tasks;
using Moq;
using Pijze.Application.Beers.Commands;
using Pijze.Application.Beers.Handlers;
using Pijze.Domain.SeedWork;
using Pijze.Domain.Services.Interfaces;
using Pijze.Domain.ValueObjects;
using Xunit;

namespace Pijze.Application.Tests.Beers.Handlers;

public class AddBeerHandlerTests
{
    [Fact]
    public async Task ShouldCallCreateMethodOnService()
    {
        var beerService = new Mock<IBeerService>();
        var imageService = new Mock<IImageService>();
        var handler = new AddBeerHandler(beerService.Object, imageService.Object);
        string name = "Test";
        Guid breweryId = Guid.NewGuid();
        int rating = 4;
        string photo = "ASDKOASODA==";
        
        await handler.HandleAsync(new AddBeer(name, breweryId, rating, photo));
        
        beerService.Verify(r => r.Create(It.IsAny<AggregateId>(), name, breweryId, rating, It.IsAny<BeerImage>()), Times.Once);
    }
}