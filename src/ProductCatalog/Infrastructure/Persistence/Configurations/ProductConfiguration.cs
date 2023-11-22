using Market.Shared.Infrastructure.Persistance.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCatalog.Domain.Product;

namespace ProductCatalog.Infrastructure.Persistence.Configurations;

public class ProductConfiguration : BaseConfiguration<Product>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Product> builder)
    {
        base.ConfigureEntity(builder);

        builder.Property(e => e.Name)
            .HasMaxLength(255);

        builder.Property(e => e.UserId)
            .IsRequired(false);

        builder.Property<decimal>("_pricePerUnit")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("PricePerUnit");

        builder.Property<decimal>("_availableStock")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("AvailableStock");

        builder.Property(e => e.PictureUri)
            .IsRequired(false);

        builder.Property(e => e.PictureFilename)
            .IsRequired(false);

        builder.HasOne(e => e.Catalog)
            .WithMany()
            .HasForeignKey(e => e.CatalogId)
            .IsRequired(false);

        builder.Property(e => e.Description)
            .IsRequired(false);

        var sellItemNavigation = builder
            .Metadata
            .FindNavigation(nameof(Product.SellUnits));

        sellItemNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.HasOne(e => e.Unit)
            .WithMany()
            .HasForeignKey(e => e.UnitId);

    }
}