using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCatalog.Domain.Product;

namespace ProductCatalog.Infrastructure.Persistence.Configurations;

public class UnitProductConfiguration : BaseProductConfiguration<Unit>
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