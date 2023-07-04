using System;
using FluentAssertions;
using Moq;
using Pijze.Domain.Exceptions;
using Pijze.Domain.Services.Interfaces;
using Pijze.Domain.ValueObjects;
using Xunit;

namespace Pijze.Domain.Tests.ValueObjects;

public class BeerImageTests
{
    private readonly Mock<IImageService> _imageService;
    
    public BeerImageTests()
    {
        _imageService = new Mock<IImageService>();
    }
    
    [Fact]
    public void ShouldThrowExceptionWhenBase64IsEmpty()
    {
        Action action = () => BeerImage.Create(string.Empty, _imageService.Object);

        action.Should().Throw<InvalidBase64FormatException>();
        _imageService.Verify(x=>x.Resize(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never);

    }
    
    [Fact]
    public void ShouldThrowExceptionWhenBase64IsNull()
    {
        Action action = () => BeerImage.Create(null!, _imageService.Object);

        action.Should().Throw<InvalidBase64FormatException>();
        _imageService.Verify(x=>x.Resize(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never);
    }
    
    [Fact]
    public void ShouldThrowExceptionWhenImageServiceIsNull()
    {
        Action action = () => BeerImage.Create("ASDKOASODA==", null!);

        action.Should().Throw<ArgumentNullException>();
        _imageService.Verify(x=>x.Resize(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never);
    }
    
    [Fact]
    public void ShouldThrowExceptionWhenBase64IsNotValid()
    {
        Action action = () => BeerImage.Create("ASDASD", _imageService.Object);

        action.Should().Throw<InvalidBase64FormatException>();
        _imageService.Verify(x=>x.Resize(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never);
    }
    
    [Fact]
    public void ShouldCreateBeerImage()
    {
        var imageBase64 = "ASDKOASODA==";
        var bytesAfterResize = new byte[] {1, 2, 3};
        var base64AfterResize = Convert.ToBase64String(bytesAfterResize);
        _imageService.Setup(x => x.Resize(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>()))
            .Returns(bytesAfterResize);
        
        var beerImage = BeerImage.Create(imageBase64, _imageService.Object);
        
        beerImage.Should().NotBeNull();
        beerImage.Bytes.Should().BeEquivalentTo(bytesAfterResize);
        beerImage.ToBase64().Should().BeEquivalentTo(base64AfterResize);
        _imageService.Verify(x=>x.Resize(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once);
    }
}