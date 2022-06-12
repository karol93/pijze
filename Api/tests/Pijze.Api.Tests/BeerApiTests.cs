using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pijze.Application.Beers.Commands;
using Pijze.Application.Beers.Dto;
using Pijze.Domain.Beers;
using Pijze.Domain.Services;
using Pijze.Infrastructure.Data;
using Pijze.Infrastructure.Images;
using Xunit;

namespace Pijze.Api.Tests;

public class BeerApiTests
{
    private readonly HttpClient _httpClient;
    private readonly PijzeDbContext _context;
    public BeerApiTests()
    {
        _context = new TestDatabase().Context;
        _httpClient = new PijzeApplication().CreateClient();
    }

    [Fact]
    public async Task ShouldReturn404StatusCodeWhenBeerDoesNotExist()
    {
        var response = await _httpClient.GetAsync($"/api/beer/{Guid.NewGuid()}");
        var statusCode = response.StatusCode;

        statusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task ShouldReturn200StatusCodeAndBeerWhenBeerExists()
    {
           
        var assembly = Assembly.GetExecutingAssembly();
        var stream = assembly.GetManifestResourceStream("Pijze.Api.Tests.Assets.800x1000.png");
        var bytes = new Byte[(int)stream.Length];

        stream.Seek(0, SeekOrigin.Begin);
        stream.Read(bytes, 0, (int)stream.Length);

        var base64 = Convert.ToBase64String(bytes);
        var beerId = "27e40feb-1039-4fa1-b1ce-ba91ec9ded0a";
        var beer = Beer.Create(Guid.Parse(beerId), "Beer", "Manufacturer", 5,
            BeerImage.Create(base64, new ImageService()));
        await _context.Database.EnsureCreatedAsync();
        _context.Beers.Add(beer); 
        await _context.SaveChangesAsync();
        
       
        Guid expectedId = new Guid("27e40feb-1039-4fa1-b1ce-ba91ec9ded0a");
        var response = await _httpClient.GetAsync($"/api/beer/{expectedId}");
        var statusCode = response.StatusCode;
        var beerDto = await response.Content.ReadFromJsonAsync<BeerDto>();
        
        statusCode.Should().Be(HttpStatusCode.OK);
        beerDto.Should().NotBeNull();
        beerDto!.Name.Should().Be(beer.Name);
        beerDto.Id.Should().Be(expectedId);
        beerDto.Manufacturer.Should().Be(beer.Manufacturer);
        beerDto.Rating.Should().Be(beer.Rating);
        beerDto.Photo.Should().Be(beer.Image.ToBase64());
    }
}