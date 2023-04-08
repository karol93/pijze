using Pijze.Application.Common.Commands;

namespace Pijze.Application.Beers.Commands;

public record AddBeer(string Name, Guid BreweryId, int Rating, string Photo) : ICommand;