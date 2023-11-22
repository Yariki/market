using Market.Shared.Infrastructure.Persistance.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCatalog.Domain.Product;

namespace ProductCatalog.Infrastructure.Persistence.Configurations;

public class SellUnitConfiguration : BaseConfiguration<SellUnit>
{
    protected override void ConfigureEntity(EntityTypeBuilder<SellUnit> builder)
    {
        base.ConfigureEntity(builder);

        builder.Property<decimal>("_scalar")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("Scalar");

        builder.HasOne(e => e.Unit)
            .WithMany()
            .HasForeignKey(e => e.UnitId);

    }
}