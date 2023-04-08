using Pijze.Application.Common.Commands;

namespace Pijze.Application.Breweries.Commands;

public record DeleteBrewery(Guid Id) : ICommand;