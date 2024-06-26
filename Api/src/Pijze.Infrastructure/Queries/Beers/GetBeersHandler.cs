﻿using Pijze.Application.Beers.Dto;
using Pijze.Application.Beers.Queries;
using Pijze.Application.Common.Queries;
using Pijze.Infrastructure.Data.DbExecutors;

namespace Pijze.Infrastructure.Queries.Beers;

internal class GetBeersHandler : IQueryHandler<GetBeers, IEnumerable<BeerListItemDto>>
{
    private readonly IDbExecutorFactory _dbExecutorFactory;
    
    public GetBeersHandler(IDbExecutorFactory dbExecutorFactory)
    {
        _dbExecutorFactory = dbExecutorFactory;
    }

    public async Task<IEnumerable<BeerListItemDto>> HandleAsync(GetBeers query)
    {
        using var db = await _dbExecutorFactory.CreateExecutor();
        var beers = await db.Query<BeerListItemDto>(
            @"SELECT beer.Id, brewery.Name as Brewery, beer.Name, beer.Rating FROM Beers beer
                 JOIN Breweries brewery on brewery.Id = beer.BreweryId
                 ORDER BY beer.CreationDate desc");
        return beers;
    }
}