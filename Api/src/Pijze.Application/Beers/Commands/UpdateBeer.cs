using Pijze.Application.Common.Commands;

namespace Pijze.Application.Beers.Commands;

public record UpdateBeer(Guid Id, string Name, string Manufacturer, int Rating, string Photo) : ICommand;