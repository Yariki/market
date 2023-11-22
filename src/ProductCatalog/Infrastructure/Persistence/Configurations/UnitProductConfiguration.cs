using Market.Shared.Infrastructure.Persistance.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCatalog.Domain.Product;

namespace ProductCatalog.Infrastructure.Persistence.Configurations;

public class UnitProductConfiguration : BaseConfiguration<Unit>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Unit> builder)
    {
        base.ConfigureEntity(builder);

        builder.Property(u => u.Abbriviation)
            .HasMaxLength(25);

        builder.Property(u => u.Description)
            .IsRequired(false)
            .HasMaxLength(255);
    }
}