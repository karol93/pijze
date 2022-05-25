using System;
using FluentAssertions;
using Moq;
using Pijze.Domain.Beers;
using Pijze.Domain.Services;
using Xunit;

namespace Pijze.Domain.Tests.Beers;

public class BeerImageTests
{
    [Fact]
    public void ShouldThrowExceptionWhenBase64IsEmpty()
    {
        Action action = () => BeerImage.Create(string.Empty, new Mock<IImageService>().Object);

        action.Should().Throw<ArgumentNullException>();
    }
    
    [Fact]
    public void ShouldCreateBeerImage()
    {
        var imageService = new Mock<IImageService>();
        var imageBase64 = "ASDKOASODA==";
        var bytesAfterResize = new byte[] {1, 2, 3};
        var base64AfterResize = Convert.ToBase64String(bytesAfterResize);
        imageService.Setup(x => x.Resize(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>()))
            .Returns(bytesAfterResize);
        
        var beerImage = BeerImage.Create(imageBase64, imageService.Object);
        
        beerImage.Should().NotBeNull();
        beerImage.Bytes.Should().BeEquivalentTo(bytesAfterResize);
        beerImage.ToBase64().Should().BeEquivalentTo(base64AfterResize);
    }
}