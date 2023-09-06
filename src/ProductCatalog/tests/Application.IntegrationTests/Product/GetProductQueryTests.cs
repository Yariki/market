using FluentAssertions;
using Market.Shared.Application.Exceptions;
using Microsoft.Extensions.DependencyInjection.Product.Commands.DeleteProduct;
using NUnit.Framework;
using ProductCatalog.Application.IntegrationTests.Data;
using ProductCatalog.Application.Product.Commands.AddProduct;

using static ProductCatalog.Application.IntegrationTests.AppicationTesting;
using ValidationException = FluentValidation.ValidationException;

namespace ProductCatalog.Application.IntegrationTests;

public class GetProductQueryTests : ApplicationBaseFixture
{
    [Test]
    public async Task GetProduct_ProductIdValidationFailed()
    {
        var getProduct = new GetProductQuery() { ProductId = Guid.Empty };

        await FluentActions
            .Invoking(() => SendAsync(getProduct))
            .Should()
            .ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task GetProduct_Success()
    {
        var id = await AddProduct("Product Name 1");

        var get = new GetProductQuery(){ProductId = id};

        var product = await SendAsync(get);

        product.Should().NotBeNull();
        product.Id.Should().Be(id);
        product.Name.Should().Be("Product Name 1");
    }

    private Task<Guid> AddProduct(string productName)
    {
        var command = new AddProductCommand()
        {
            Name = productName,
            UnitId = SeedData.Unit1Id1,
            CatalogId = SeedData.Catalog1Id,
            PricePerUnit = 1,
            AvailableStock = 1,
            Description = "Desc",
            PictureFilename = "filename",
            PictureUri = "pictureuri"
        };

        return SendAsync(command);
    }
    
}