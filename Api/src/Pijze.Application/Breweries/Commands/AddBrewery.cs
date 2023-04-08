using Pijze.Application.Common.Commands;

namespace Pijze.Application.Breweries.Commands;

public record AddBrewery(string Name) : ICommand;