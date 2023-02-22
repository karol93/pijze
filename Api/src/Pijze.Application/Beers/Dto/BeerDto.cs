namespace Pijze.Application.Beers.Dto;

public record BeerDto(string Id, string Manufacturer, string Name, int Rating, byte[] Photo);
