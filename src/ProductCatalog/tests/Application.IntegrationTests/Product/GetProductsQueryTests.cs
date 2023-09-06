using FluentAssertions;
using Market.Shared.Application.Exceptions;
using Microsoft.Extensions.DependencyInjection.Product.Commands.DeleteProduct;
using Microsoft.Extensions.DependencyInjection.Product.Queries;
using NUnit.Framework;
using ProductCatalog.Application.IntegrationTests.Data;
using ProductCatalog.Application.Product.Commands.AddProduct;

using static ProductCatalog.Application.IntegrationTests.AppicationTesting;
using ValidationException = FluentValidation.ValidationException;

namespace ProductCatalog.Application.IntegrationTests;

public class GetProductsQueryTests : ApplicationBaseFixture
{

    [Test]
    public async Task GetProducts_WithOutCategorySuccess()
    {
        await AddProduct("Product 1");
        await AddProduct("Product 2");
        await AddProduct("Product 3");

        var cmd = new GetProductsQuery() { CatalogId = Guid.Empty };

        var products = await SendAsync(cmd);

        products.Should().NotBeEmpty();
        products.Count().Should().Be(3);

    }
    
    [Test]
    public async Task GetProducts_WithCategorySuccess()
    {
        await AddProduct("Product 1", SeedData.Catalog1Id);
        await AddProduct("Product 2", SeedData.Catalog1Id);
        await AddProduct("Product 3", SeedData.Catalog2Id);

        var cmd = new GetProductsQuery() { CatalogId = SeedData.Catalog1Id };

        var products = await SendAsync(cmd);

        products.Should().NotBeEmpty();
        products.Count().Should().Be(2);

    }
    
    private Task<Guid> AddProduct(string productName, Guid? catalogId = null)
    {
        var command = new AddProductCommand()
        {
            Name = productName,
            UnitId = SeedData.Unit1Id1,
            CatalogId = catalogId, 
            PricePerUnit = 1,
            AvailableStock = 1,
            Description = "Desc",
            PictureFilename = "filename",
            PictureUri = "pictureuri"
        };

        return SendAsync(command);
    }
}