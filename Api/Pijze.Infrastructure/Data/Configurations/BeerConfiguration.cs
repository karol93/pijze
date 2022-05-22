using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pijze.Domain.Beers;

namespace Pijze.Infrastructure.Data.Configurations;

public class BeerConfiguration : IEntityTypeConfiguration<Beer>
{
    public void Configure(EntityTypeBuilder<Beer> builder)
    {
        builder.OwnsOne(x => x.Image, c =>
        {
            c.Property(p => p.Bytes).HasColumnName(nameof(Beer.Image));
        });
    }
}