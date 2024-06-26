﻿using Microsoft.EntityFrameworkCore;
using Pijze.Domain.Entities;
using Pijze.Domain.Repositories;
using Pijze.Domain.SeedWork;

namespace Pijze.Infrastructure.Data.EntityFramework.Repositories;

internal class BreweryRepository : IBreweryRepository
{
    private readonly DbSet<Brewery> _breweries;

    public BreweryRepository(PijzeDbContext context)
    {
        _breweries = context.Breweries;
    }

    public async Task<bool> Exists(AggregateId id) => await _breweries.AnyAsync(x => x.Id == id);

    public async Task<Brewery?> Find(AggregateId id) => await _breweries.FirstOrDefaultAsync(x => x.Id == id);

    public void Add(Brewery brewery) => _breweries.Add(brewery);

    public void Delete(Brewery brewery) => _breweries.Remove(brewery);
}