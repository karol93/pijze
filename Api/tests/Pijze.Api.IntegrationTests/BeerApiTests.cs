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
        var command = new AddBeer("name", breweryId, 4, "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/w8AAwAB/7+lU7QAAAAASUVORK5CYII=");
        _commandDispatcher.Setup(x => x.SendAsync(command));
        await using var app = new TestApplicationFactory(x =>
        {
            x.AddScoped(typeof(IBreweryRepository), _=>breweryRepository.Object);
            x.AddScoped(typeof(IBeerRepository), _=>beerRepository.Object);
        });
        var httpClient = app.CreateClient();
        
        var response = await httpClient.PostAsync($"api/beer", new StringContent(JsonSerializer.Serialize(command),
            Encoding.UTF8, "application/json"));

        (await response.Content.ReadAsStringAsync()).Should().Be("tt");
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