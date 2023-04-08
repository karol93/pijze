using Pijze.Application.Beers.Dto;
using Pijze.Application.Beers.Queries;
using Pijze.Application.Common.Queries;
using Pijze.Infrastructure.Data.DbExecutors;

namespace Pijze.Infrastructure.Queries.Beers;

internal class FindBeerHandler : IQueryHandler<FindBeer, BeerDto?>
{
    private readonly IDbExecutorFactory _dbExecutorFactory;
    
    public FindBeerHandler(IDbExecutorFactory dbExecutorFactory)
    {
        _dbExecutorFactory = dbExecutorFactory;
    }

    public async Task<BeerDto?> HandleAsync(FindBeer query)
    {
        using var db = await _dbExecutorFactory.CreateExecutor();
        var beer = await db.QueryFirstOrDefault<BeerDto>(
            @"SELECT beer.Id, brewery.Name as Manufacturer, beer.Name, beer.Rating, beer.Image as Photo 
                  FROM Beers beer
                  JOIN Breweries brewery on beer.BreweryId = brewery.Id
                  where beer.Id = @beerId",
            new {beerId = query.Id});
        return beer;
    }
}