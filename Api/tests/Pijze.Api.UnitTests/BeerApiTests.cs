using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Pijze.Api.Apis;
using Pijze.Application.Beers.Commands;
using Pijze.Application.Beers.Dto;
using Pijze.Application.Beers.Queries;
using Pijze.Infrastructure.Commands;
using Pijze.Infrastructure.Queries;

namespace Pijze.Api.UnitTests;

public class BeerApiTests
{
    private readonly BeerApi _beerApi = new();
    private readonly Mock<IQueryDispatcher> _queryDispatcher = new();
    private readonly Mock<ICommandDispatcher> _commandDispatcher = new();
    
    [Fact]
    public async Task Get_ReturnBeer_WhenBeerExists()
    {
        var guid = Guid.NewGuid();
        var query = new FindBeer(guid);
        var beerDto = new BeerDto(guid.ToString(), "manufacuturer", "name", 3, new byte[] { });
        _queryDispatcher.Setup(x => x.QueryAsync(query))
            .ReturnsAsync(beerDto);

        var result = await _beerApi.Get(guid, _queryDispatcher.Object);

        var typedResult = result.Result.Should().BeOfType<Ok<BeerDto>>().Subject;
        typedResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        typedResult.Value.Should().Be(beerDto);
    }
    
    [Fact]
    public async Task Get_ReturnNotFound_WhenBeerDoesNotExist()
    {
        var guid = Guid.NewGuid();
        var query = new FindBeer(guid);
        _queryDispatcher.Setup(x => x.QueryAsync(query))
            .ReturnsAsync((BeerDto?)null);

        var result = await _beerApi.Get(guid, _queryDispatcher.Object);

        var typedResult = result.Result.Should().BeOfType<NotFound>().Subject;
        typedResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }
    
    [Fact]
    public async Task GetBeerImage_ReturnImage_WhenBeerExists()
    {
        var guid = Guid.NewGuid();
        var query = new GetBeerImage(guid);
        var image = new byte[] {0x40};
        
        _queryDispatcher.Setup(x => x.QueryAsync(query))
            .ReturnsAsync(image);

        var result = await _beerApi.GetBeerImage(guid, _queryDispatcher.Object);

        var typedResult = result.Result.Should().BeOfType<FileContentHttpResult>().Subject;
        typedResult.ContentType.Should().Be("image/jpeg");
        typedResult.FileLength.Should().Be(image.Length);
        typedResult.FileContents.ToArray().Should().BeEquivalentTo(image);
    }
    
    [Fact]
    public async Task GetBeerImage_ReturnNotFound_WhenBeerDoesNotExist()
    {
        var guid = Guid.NewGuid();
        var query = new GetBeerImage(guid);

        _queryDispatcher.Setup(x => x.QueryAsync(query))
            .ReturnsAsync((byte[]?) null);

        var result = await _beerApi.GetBeerImage(guid, _queryDispatcher.Object);

        var typedResult = result.Result.Should().BeOfType<NotFound>().Subject;
        typedResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }
    
    [Fact]
    public async Task GetAll_ReturnBeers_WhenAtLeastOneBeerExists()
    {
        var guid = Guid.NewGuid();
        var query = new GetBeers();
        var beerListItemDto = new BeerListItemDto(guid.ToString(), "manufacuturer", "name", 3);
        var beerListDto = new List<BeerListItemDto>()
        {
            beerListItemDto
        };
        _queryDispatcher.Setup(x => x.QueryAsync(query))
            .ReturnsAsync(beerListDto);

        var result = await _beerApi.GetAll(_queryDispatcher.Object);

        var typedResult = result.Should().BeOfType<Ok<IEnumerable<BeerListItemDto>>>().Subject;
        typedResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        typedResult.Value.Should().NotBeNull();
        typedResult.Value.Should().NotBeEmpty();
        typedResult.Value!.Count().Should().Be(1);
        typedResult.Value.Should().BeEquivalentTo(beerListDto);
    }
    
    [Fact]
    public async Task GetAll_ReturnEmptyList_WhenNoBeers()
    {
        var query = new GetBeers();
        _queryDispatcher.Setup(x => x.QueryAsync(query))
            .ReturnsAsync(new List<BeerListItemDto>());

        var result = await _beerApi.GetAll(_queryDispatcher.Object);

        var typedResult = result.Should().BeOfType<Ok<IEnumerable<BeerListItemDto>>>().Subject;
        typedResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        typedResult.Value.Should().NotBeNull();
        typedResult.Value.Should().BeEmpty();
        typedResult.Value!.Count().Should().Be(0);
    }
    
    [Fact]
    public async Task Create_ReturnOk_WhenBeerWasCreated()
    {
        var command = new AddBeer("",Guid.NewGuid(), 3,"qweq");

        var result = await _beerApi.Create(command, _commandDispatcher.Object);

        var typedResult = result.Result.Should().BeOfType<Ok>().Subject;
        typedResult.StatusCode.Should().Be(StatusCodes.Status200OK);
    }
    
    [Fact]
    public async Task Update_ReturnNoContent_WhenBeerWasUpdated()
    {
        var guid = Guid.NewGuid();
        var command = new UpdateBeer(guid,"name",Guid.NewGuid(), 3,"qweq");

        var result = await _beerApi.Update(guid, command, _commandDispatcher.Object);

        var typedResult = result.Result.Should().BeOfType<NoContent>().Subject;
        typedResult.StatusCode.Should().Be(StatusCodes.Status204NoContent);
    }
    
    [Fact]
    public async Task Delete_ReturnNoContent_WhenBeerWasDeleted()
    {
        var result = await _beerApi.Delete(Guid.NewGuid(), _commandDispatcher.Object);

        var typedResult = result.Result.Should().BeOfType<NoContent>().Subject;
        typedResult.StatusCode.Should().Be(StatusCodes.Status204NoContent);
    }
}