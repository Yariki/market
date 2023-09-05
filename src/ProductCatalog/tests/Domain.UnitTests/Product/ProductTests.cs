using FluentAssertions;
using NUnit.Framework;
using ProductCatalog.Domain.Product;
using ProductEntity = ProductCatalog.Domain.Product.Product;

namespace ProductCatalog.Domain.UnitTests.Product;

public class ProductTests
{
    private const string UserId = "user-id";

    [Test]
    public void CreateProduct()
    {
        var unit = Unit.CreateUnit("kg", description: "Kilogram");

        var product = CreateProduct(
            unit.Id,
            10,
            null,
            10,
            "pictureUri",
            "pictureFilename",
            "description");

        product.Should().NotBeNull();
        product.Name.Should().Be("product");
        product.UserId.Should().Be(UserId);
        product.UnitId.Should().Be(unit.Id);
        product.PricePerUnit.Should().Be(10);
        product.CatalogId.Should().BeNull();
        product.AvailableStock.Should().Be(10);
        product.PictureUri.Should().Be("pictureUri");
        product.PictureFilename.Should().Be("pictureFilename");
        product.Description.Should().Be("description");
    }







    private ProductEntity CreateProduct(Guid unitId,
        decimal? pricePerUnit,
        Guid? catalogId,
        decimal? availableInStock,
        string pictureUri,
        string pictureFilename,
        string description)
    {
        var product = new ProductEntity("product", UserId, unitId,
            pricePerUnit ?? 10,
            catalogId ?? null,
            availableInStock ?? 10,
            pictureUri ?? string.Empty,
            pictureFilename ?? string.Empty,
            description ?? string.Empty);
        return product;
    }

}
