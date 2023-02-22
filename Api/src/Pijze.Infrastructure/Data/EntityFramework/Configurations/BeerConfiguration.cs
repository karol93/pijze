using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pijze.Domain.Beers;

namespace Pijze.Infrastructure.Data.EntityFramework.Configurations;

public class BeerConfiguration : IEntityTypeConfiguration<Beer>
{
    public void Configure(EntityTypeBuilder<Beer> builder)
    {
        builder.Property(x=>x.Id).HasConversion(
            g => g.ToString().ToLowerInvariant(),
            s => new Guid(s));
        builder.OwnsOne(x => x.Image, c =>
        {
            c.Property(p => p.Bytes).HasColumnName(nameof(Beer.Image));
        });
    }
}