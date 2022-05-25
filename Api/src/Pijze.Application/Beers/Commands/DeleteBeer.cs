using Pijze.Application.Common.Commands;

namespace Pijze.Application.Beers.Commands;

public record DeleteBeer(Guid Id) : ICommand;