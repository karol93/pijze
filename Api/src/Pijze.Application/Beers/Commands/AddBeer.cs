using Pijze.Application.Common.Commands;

namespace Pijze.Application.Beers.Commands;

public record AddBeer(string Name, string Manufacturer, int Rating, string Photo) : ICommand;