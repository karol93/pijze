using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Pijze.Api.Apis;
using Pijze.Application.Breweries.Commands;
using Pijze.Application.Breweries.Dto;
using Pijze.Application.Breweries.Queries;
using Pijze.Infrastructure.Commands;
using Pijze.Infrastructure.Queries;

namespace Pijze.Api.UnitTests;

public class BreweryApiTests
{
    private readonly BreweryApi _breweryApi = new();
    private readonly Mock<IQueryDispatcher> _queryDispatcher = new();
    private readonly Mock<ICommandDispatcher> _commandDispatcher = new();

    [Fact]
    public async Task Get_ReturnBrewery_WhenBreweryExists()
    {
        var guid = Guid.NewGuid();
        var query = new FindBrewery(guid);
        var breweryDto = new BreweryDto(guid.ToString(), "name");
        _queryDispatcher.Setup(x => x.QueryAsync(query))
            .ReturnsAsync(breweryDto);

        var result = await _breweryApi.Get(guid, _queryDispatcher.Object);

        var typedResult = result.Result.Should().BeOfType<Ok<BreweryDto>>().Subject;
        typedResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        typedResult.Value.Should().Be(breweryDto);
    }

    [Fact]
    public async Task Get_ShouldReturnNotFound_WhenBreweryNotFound()
    {
        var testId = Guid.NewGuid();
        _queryDispatcher.Setup(x => x.QueryAsync(new FindBrewery(testId)))
            .ReturnsAsync((BreweryDto?)null);

        var breweryApi = new BreweryApi();

        var result = await breweryApi.Get(testId, _queryDispatcher.Object);

        var typedResult = result.Result.Should().BeOfType<NotFound>().Subject;
        typedResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }
    
    [Fact]
    public async Task GetAll_ReturnBreweries_WhenAtLeastOneBreweryExists()
    {
        var guid = Guid.NewGuid();
        var query = new GetBreweries();
        var breweryListItemDto = new BreweryDto(guid.ToString(), "name");
        var breweryListDto = new List<BreweryDto>()
        {
            breweryListItemDto
        };
        _queryDispatcher.Setup(x => x.QueryAsync(query))
            .ReturnsAsync(breweryListDto);

        var result = await _breweryApi.GetAll(_queryDispatcher.Object);

        var typedResult = result.Should().BeOfType<Ok<IEnumerable<BreweryDto>>>().Subject;
        typedResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        typedResult.Value.Should().NotBeNull();
        typedResult.Value.Should().NotBeEmpty();
        typedResult.Value!.Count().Should().Be(1);
        typedResult.Value.Should().BeEquivalentTo(breweryListDto);
    }
    
    [Fact]
    public async Task GetAll_ReturnEmptyList_WhenNoBreweries()
    {
        var query = new GetBreweries();
        _queryDispatcher.Setup(x => x.QueryAsync(query))
            .ReturnsAsync(new List<BreweryDto>());

        var result = await _breweryApi.GetAll(_queryDispatcher.Object);

        var typedResult = result.Should().BeOfType<Ok<IEnumerable<BreweryDto>>>().Subject;
        typedResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        typedResult.Value.Should().NotBeNull();
        typedResult.Value.Should().BeEmpty();
        typedResult.Value!.Count().Should().Be(0);
    }
    
    [Fact]
    public async Task Create_ReturnOk_WhenBreweryWasCreated()
    {
        var breweryName = "name";
        var breweryId = Guid.NewGuid();
        var command = new AddBrewery(breweryName);
        var breweryDto = new BreweryDto(breweryId.ToString(), breweryName);
        _commandDispatcher.Setup(x => x.SendAsync<AddBrewery,Guid>(command))
            .ReturnsAsync(breweryId);
        _queryDispatcher.Setup(x => x.QueryAsync(new FindBrewery(breweryId)))
            .ReturnsAsync(breweryDto);
        
        var result = await _breweryApi.Create(command, _commandDispatcher.Object, _queryDispatcher.Object);

        var typedResult = result.Result.Should().BeOfType<CreatedAtRoute<BreweryDto>>().Subject;
        typedResult.StatusCode.Should().Be(StatusCodes.Status201Created);
        typedResult.Value.Should().NotBeNull();
        typedResult.Value.Should().Be(breweryDto);
    }
    
    [Fact]
    public async Task Update_ReturnNoContent_WhenBreweryWasUpdated()
    {
        var guid = Guid.NewGuid();
        var command = new UpdateBrewery(guid, "name");

        var result = await _breweryApi.Update(guid, command, _commandDispatcher.Object);

        var typedResult = result.Should().BeOfType<NoContent>().Subject;
        typedResult.StatusCode.Should().Be(StatusCodes.Status204NoContent);
    }
    
    [Fact]
    public async Task Update_ReturnNoContent_WhenBreweryWasDeleted()
    {
        var result = await _breweryApi.Delete(Guid.NewGuid(), _commandDispatcher.Object);

        var typedResult = result.Should().BeOfType<NoContent>().Subject;
        typedResult.StatusCode.Should().Be(StatusCodes.Status204NoContent);
    }

}