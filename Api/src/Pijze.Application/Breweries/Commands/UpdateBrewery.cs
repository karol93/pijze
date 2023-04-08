using Pijze.Application.Common.Commands;

namespace Pijze.Application.Breweries.Commands;

public record UpdateBrewery(Guid Id, string Name) : ICommand;