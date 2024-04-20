namespace Pijze.Application.Beers.Dto;

public record BeerDto(string Id, string BreweryId, string Name, int Rating, byte[] Photo);
