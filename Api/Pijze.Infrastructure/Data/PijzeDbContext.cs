using Microsoft.EntityFrameworkCore;
using Pijze.Domain.Beers;
using Pijze.Infrastructure.Data.Configurations;

namespace Pijze.Infrastructure.Data;

internal class PijzeDbContext : DbContext
{
    public PijzeDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Beer> Beers => Set<Beer>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new BeerConfiguration());
    }
}