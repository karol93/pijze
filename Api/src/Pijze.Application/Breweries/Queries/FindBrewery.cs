using Pijze.Application.Breweries.Dto;
using Pijze.Application.Common.Queries;

namespace Pijze.Application.Breweries.Queries;

public record FindBrewery(Guid Id) : IQuery<BreweryDto>;