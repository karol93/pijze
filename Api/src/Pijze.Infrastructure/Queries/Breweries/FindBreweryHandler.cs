using Pijze.Application.Breweries.Dto;
using Pijze.Application.Breweries.Queries;
using Pijze.Application.Common.Queries;
using Pijze.Infrastructure.Data.DbExecutors;

namespace Pijze.Infrastructure.Queries.Breweries;

internal class FindBreweryHandler : IQueryHandler<FindBrewery, BreweryDto?>
{
    private readonly IDbExecutorFactory _dbExecutorFactory;
    
    public FindBreweryHandler(IDbExecutorFactory dbExecutorFactory)
    {
        _dbExecutorFactory = dbExecutorFactory;
    }

    public async Task<BreweryDto?> HandleAsync(FindBrewery query)
    {
        using var db = await _dbExecutorFactory.CreateExecutor();
        var brewery = await db.QueryFirstOrDefault<BreweryDto>(
            @"SELECT brewery.Id, brewery.Name
                  FROM Breweries brewery
                  where brewery.Id = @breweryId",
            new {breweryId = query.Id});
        return brewery;
    }
}