using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pijze.Domain.Entities;

namespace Pijze.Infrastructure.Data.EntityFramework.Configurations;

internal class BreweryConfiguration : IEntityTypeConfiguration<Brewery>
{
    public void Configure(EntityTypeBuilder<Brewery> builder)
    {
        builder.Property(x => x.Id).HasConversion(
            g => g.ToString().ToLowerInvariant(),
            s => new Guid(s));
        builder.Property(x => x.Name).IsRequired();
    }
}