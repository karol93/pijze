using System.Threading.Tasks;
using Moq;
using Pijze.Application.Beers.Commands;
using Pijze.Application.Beers.Handlers;
using Pijze.Domain.Services;
using Xunit;

namespace Pijze.Application.Tests.Beers.Handlers;

public class AddBeerHandlerTests
{
    [Fact]
    public async Task ShouldCallAddMethodOnRepository()
    {
        // var repository = new Mock<IBeerRepository>();
        // var handler = new AddBeerHandler(repository.Object, new Mock<IImageService>().Object);
        //
        // await handler.HandleAsync(new AddBeer("Test", "test", 4, "ASDKOASODA=="));
        //
        // repository.Verify(r => r.Add(It.IsAny<Beer>()), Times.Once);
    }
}