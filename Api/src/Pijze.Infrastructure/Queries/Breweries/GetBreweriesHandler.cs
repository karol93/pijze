using Pijze.Application.Beers.Dto;
using Pijze.Application.Beers.Queries;
using Pijze.Application.Breweries.Dto;
using Pijze.Application.Breweries.Queries;
using Pijze.Application.Common.Queries;
using Pijze.Infrastructure.Data.DbExecutors;

namespace Pijze.Infrastructure.Queries.Breweries;

internal class GetBreweriesHandler : IQueryHandler<GetBreweries, IEnumerable<BreweryDto>>
{
    private readonly IDbExecutorFactory _dbExecutorFactory;

    public GetBreweriesHandler(IDbExecutorFactory dbExecutorFactory)
    {
        _dbExecutorFactory = dbExecutorFactory;
    }

    public async Task<IEnumerable<BreweryDto>> HandleAsync(GetBreweries query)
    {
        using var db = await _dbExecutorFactory.CreateExecutor();
        var breweries = await db.Query<BreweryDto>(
            @"SELECT brewery.Id, brewery.Name FROM Breweries brewery");
        return breweries;
    }
}