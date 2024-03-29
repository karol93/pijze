using System.Net;
using System.Text;
using System.Text.Json;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Pijze.Application.Beers.Commands;
using Pijze.Application.Beers.Dto;
using Pijze.Application.Beers.Queries;
using Pijze.Domain.Entities;
using Pijze.Domain.Repositories;
using Pijze.Infrastructure.Commands;
using Pijze.Infrastructure.Queries;
using Pijze.Tests.Shared.Builders.Entities;

namespace Pijze.Api.IntegrationTests;

public class BeerApiTests
{
    private readonly Mock<IQueryDispatcher> _queryDispatcher = new();
    private readonly Mock<ICommandDispatcher> _commandDispatcher = new();
    
    [Fact]
    public async Task GetBeerById_ReturnNotFound_WhenBeerDoesNotExist()
    {
        var beerId = Guid.NewGuid();
        var query = new FindBeer(beerId);
        _queryDispatcher.Setup(x => x.QueryAsync(query)).ReturnsAsync((BeerDto?) null);
        await using var app = new TestApplicationFactory(x =>
        {
            x.AddSingleton(_queryDispatcher.Object);
        });
        var httpClient = app.CreateClient();
        
        var response = await httpClient.GetAsync($"api/beer/{beerId}");
        
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task GetBeerById_ReturnBeer_WhenBeerExists()
    {
        var beerId = Guid.NewGuid();
        var query = new FindBeer(beerId);
        var beerDto = new BeerDto(beerId.ToString(), "manufacutorer", "name", 4, new byte[] { });
        _queryDispatcher.Setup(x => x.QueryAsync(query)).ReturnsAsync(beerDto);
        await using var app = new TestApplicationFactory(x =>
        {
            x.AddSingleton(_queryDispatcher.Object);
        });
        var httpClient = app.CreateClient();
        
        var response = await httpClient.GetAsync($"api/beer/{beerId}");
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var responseText = await response.Content.ReadAsStringAsync();
        var beerDtoResult = JsonSerializer.Deserialize<BeerDto>(responseText, JsonOptions.Get);
        beerDtoResult.Should().BeEquivalentTo(beerDto);
    }
    
    [Fact]
    public async Task GetBeerImage_ReturnNotFound_WhenBeerDoesNotExist()
    {
        var beerId = Guid.NewGuid();
        var query = new GetBeerImage(beerId);
        _queryDispatcher.Setup(x => x.QueryAsync(query)).ReturnsAsync((byte[]?) null);
        await using var app = new TestApplicationFactory(x =>
        {
            x.AddSingleton(_queryDispatcher.Object);
        });
        var httpClient = app.CreateClient();
        
        var response = await httpClient.GetAsync($"api/beer/{beerId}/image");
        
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task GetBeerImage_ReturnImage_WhenBeerExists()
    {
        var beerId = Guid.NewGuid();
        var query = new GetBeerImage(beerId);
        var expectedResult = new byte[] {0xFF, 0x80, 0x40, 0x00};
        _queryDispatcher.Setup(x => x.QueryAsync(query)).ReturnsAsync(expectedResult);
        await using var app = new TestApplicationFactory(x =>
        {
            x.AddSingleton(_queryDispatcher.Object);
        });
        var httpClient = app.CreateClient();
        
        var response = await httpClient.GetAsync($"api/beer/{beerId}/image");
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Content.Headers.ContentType.ToString().Should().Be("image/jpeg");
        response.Content.Headers.ContentLength.Should().Be(expectedResult.Length);
        var responseByte = await response.Content.ReadAsByteArrayAsync();
        responseByte.Should().BeEquivalentTo(expectedResult);
    }
    
    [Fact]
    public async Task GetAll_ReturnEmptyArray_WhenBeersDoNotExist()
    {
        var query = new GetBeers();
        _queryDispatcher.Setup(x => x.QueryAsync(query)).ReturnsAsync(new List<BeerListItemDto>());
        await using var app = new TestApplicationFactory(x =>
        {
            x.AddSingleton(_queryDispatcher.Object);
        });
        var httpClient = app.CreateClient();
        
        var response = await httpClient.GetAsync($"api/beer");
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var responseText = await response.Content.ReadAsStringAsync();
        var beersResult = JsonSerializer.Deserialize<IList<BeerListItemDto>>(responseText, JsonOptions.Get);
        beersResult.Should().NotBeNull();
        beersResult.Count().Should().Be(0);
    }
    
    [Fact]
    public async Task GetAll_ReturnTwoBeers_WhenBeersExists()
    {
        var query = new GetBeers();
        var expectedResult = new List<BeerListItemDto>()
        {
            new(Guid.NewGuid().ToString(), "m1", "n1", 1),
            new(Guid.NewGuid().ToString(), "m2", "n2", 2),
        };
        _queryDispatcher.Setup(x => x.QueryAsync(query)).ReturnsAsync(expectedResult);
        await using var app = new TestApplicationFactory(x =>
        {
            x.AddSingleton(_queryDispatcher.Object);
        });
        var httpClient = app.CreateClient();
        
        var response = await httpClient.GetAsync($"api/beer");
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var responseText = await response.Content.ReadAsStringAsync();
        var beersResult = JsonSerializer.Deserialize<IList<BeerListItemDto>>(responseText, JsonOptions.Get);
        beersResult.Should().BeEquivalentTo(expectedResult);
    }
    
    [Fact]
    public async Task Create_ReturnOk_WhenBeerCreated()
    {
        var breweryId = Guid.NewGuid();
        var breweryRepository = new Mock<IBreweryRepository>();
        breweryRepository.Setup(x => x.Exists(breweryId)).ReturnsAsync(true);
        var beerRepository = new Mock<IBeerRepository>();
        var command = new AddBeer("name", breweryId, 4, "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");
        _commandDispatcher.Setup(x => x.SendAsync(command));
        await using var app = new TestApplicationFactory(x =>
        {
            x.AddScoped(typeof(IBreweryRepository), _=>breweryRepository.Object);
            x.AddScoped(typeof(IBeerRepository), _=>beerRepository.Object);
        });
        var httpClient = app.CreateClient();
        
        var response = await httpClient.PostAsync($"api/beer", new StringContent(JsonSerializer.Serialize(command),
            Encoding.UTF8, "application/json"));

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task Create_ReturnBadRequest_WhenNameIsEmpty()
    {
        var command = new AddBeer("", Guid.NewGuid(), 4, "qwe");
        await using var app = new TestApplicationFactory();
        var httpClient = app.CreateClient();
        
        var response = await httpClient.PostAsync($"api/beer", new StringContent(JsonSerializer.Serialize(command),
            Encoding.UTF8, "application/json"));
        
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-51)]
    [InlineData(6)]
    public async Task Create_ReturnBadRequest_WhenRatingIsInvalid(int rating)
    {
        var command = new AddBeer("name", Guid.NewGuid(), rating, "qwe");
        await using var app = new TestApplicationFactory();
        var httpClient = app.CreateClient();
        
        var response = await httpClient.PostAsync($"api/beer", new StringContent(JsonSerializer.Serialize(command),
            Encoding.UTF8, "application/json"));
        
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task Create_ReturnBadRequest_WhenImageIsEmpty()
    {
        var command = new AddBeer("name", Guid.NewGuid(), 4, "");
        await using var app = new TestApplicationFactory();
        var httpClient = app.CreateClient();
        
        var response = await httpClient.PostAsync($"api/beer", new StringContent(JsonSerializer.Serialize(command),
            Encoding.UTF8, "application/json"));
        
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task Delete_ReturnNoContent_WhenBeerWasDeleted()
    {
        var beerId = Guid.NewGuid();
        var beerRepository = new Mock<IBeerRepository>();
        beerRepository.Setup(x => x.Find(beerId)).ReturnsAsync(new BeerBuilder().Build());
        await using var app = new TestApplicationFactory(x =>
        {
            x.AddScoped(typeof(IBeerRepository), _=>beerRepository.Object);
        });
        var httpClient = app.CreateClient();
        
        var response = await httpClient.DeleteAsync($"api/beer/{beerId}");
        
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
    
    [Fact]
    public async Task Delete_ReturnBadRequest_WhenBeerDoesNotExist()
    {
        var beerId = Guid.NewGuid();
        var beerRepository = new Mock<IBeerRepository>();
        beerRepository.Setup(x => x.Find(beerId)).ReturnsAsync((Beer?)null);
        await using var app = new TestApplicationFactory(x =>
        {
            x.AddScoped(typeof(IBeerRepository), _=>beerRepository.Object);
        });
        var httpClient = app.CreateClient();
        
        var response = await httpClient.DeleteAsync($"api/beer/{beerId}");
        
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}