using Microsoft.EntityFrameworkCore;
using Pijze.Domain.Entities;
using Pijze.Infrastructure.Data.EntityFramework.Configurations;

namespace Pijze.Infrastructure.Data.EntityFramework;

internal class PijzeDbContext : DbContext
{
    public PijzeDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Beer> Beers => Set<Beer>();
    public DbSet<Brewery> Breweries => Set<Brewery>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PijzeDbContext).Assembly);
    }
}