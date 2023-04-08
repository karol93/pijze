using Pijze.Application.Breweries.Dto;
using Pijze.Application.Common.Queries;

namespace Pijze.Application.Breweries.Queries;

public record GetBreweries : IQuery<IEnumerable<BreweryDto>>;