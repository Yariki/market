using FluentAssertions;
using Market.Shared.Application.Exceptions;
using Microsoft.Extensions.DependencyInjection.Product.Commands.UpdateProduct;
using NUnit.Framework;
using ProductCatalog.Application.IntegrationTests.Data;
using ProductCatalog.Application.Product.Commands.AddProduct;
using static ProductCatalog.Application.IntegrationTests.AppicationTesting;
using ValidationException = FluentValidation.ValidationException;

namespace ProductCatalog.Application.IntegrationTests;

public class UpdateProductCommandTests : ApplicationBaseFixture
{
    [Test]
    public async Task UpdateProduct_ProductIdValidationFailed()
    {
        var updateCommand = new UpdateProductCommand()
        {
            ProductId = Guid.Empty,
            Name = "Name",
            AvailableStock = 1,
            PricePerUnit = 1,
            UnitId = SeedData.Unit1Id1,
            Description = "desc",
            PictureUri = "uri",
            PictureFilename = "filename",
            MaxStockThreshold = 1,
            CatalogId = SeedData.Catalog1Id
        };

        await FluentActions
            .Invoking(() => SendAsync(updateCommand))
            .Should()
            .ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task UpdateProduct_NameValidationFailed()
    {
        var updateCommand = new UpdateProductCommand()
        {
            ProductId = Guid.NewGuid(),
            Name = string.Empty,
            AvailableStock = 1,
            PricePerUnit = 1,
            UnitId = SeedData.Unit1Id1,
            Description = "desc",
            PictureUri = "uri",
            PictureFilename = "filename",
            MaxStockThreshold = 1,
            CatalogId = SeedData.Catalog1Id
        };

        await FluentActions
            .Invoking(() => SendAsync(updateCommand))
            .Should()
            .ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task UpdateProduct_UnitIdValidationFailed()
    {
        var updateCommand = new UpdateProductCommand()
        {
            ProductId = Guid.NewGuid(),
            Name = "Name",
            AvailableStock = 1,
            PricePerUnit = 1,
            UnitId = Guid.Empty,
            Description = "desc",
            PictureUri = "uri",
            PictureFilename = "filename",
            MaxStockThreshold = 1,
            CatalogId = SeedData.Catalog1Id
        };

        await FluentActions
            .Invoking(() => SendAsync(updateCommand))
            .Should()
            .ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task UpdateProduct_PricePerUnitValidationFailed()
    {
        var updateCommand = new UpdateProductCommand()
        {
            ProductId = Guid.NewGuid(),
            Name = "Name",
            AvailableStock = 1,
            PricePerUnit = 0,
            UnitId = SeedData.Unit1Id1,
            Description = "desc",
            PictureUri = "uri",
            PictureFilename = "filename",
            MaxStockThreshold = 1,
            CatalogId = SeedData.Catalog1Id
        };

        await FluentActions
            .Invoking(() => SendAsync(updateCommand))
            .Should()
            .ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task UpdateProduct_AvailableInStockValidationFailed()
    {
        var updateCommand = new UpdateProductCommand()
        {
            ProductId = Guid.NewGuid(),
            Name = "Name",
            AvailableStock = 0,
            PricePerUnit = 1,
            UnitId = SeedData.Unit1Id1,
            Description = "desc",
            PictureUri = "uri",
            PictureFilename = "filename",
            MaxStockThreshold = 1,
            CatalogId = SeedData.Catalog1Id
        };

        await FluentActions
            .Invoking(() => SendAsync(updateCommand))
            .Should()
            .ThrowAsync<ValidationException>();
    }


    [Test]
    public async Task UpdateProduct_DescriptionValidationFailed()
    {
        var updateCommand = new UpdateProductCommand()
        {
            ProductId = Guid.NewGuid(),
            Name = "Name",
            AvailableStock = 1,
            PricePerUnit = 1,
            UnitId = SeedData.Unit1Id1,
            Description = string.Empty,
            PictureUri = "uri",
            PictureFilename = "filename",
            MaxStockThreshold = 1,
            CatalogId = SeedData.Catalog1Id
        };

        await FluentActions
            .Invoking(() => SendAsync(updateCommand))
            .Should()
            .ThrowAsync<ValidationException>();
    }


    [Test]
    public async Task UpdateProduct_PictureFilenameValidationFailed()
    {
        var updateCommand = new UpdateProductCommand()
        {
            ProductId = Guid.NewGuid(),
            Name = "Name",
            AvailableStock = 1,
            PricePerUnit = 1,
            UnitId = SeedData.Unit1Id1,
            Description = "desc",
            PictureUri = "uri",
            PictureFilename = string.Empty,
            MaxStockThreshold = 1,
            CatalogId = SeedData.Catalog1Id
        };

        await FluentActions
            .Invoking(() => SendAsync(updateCommand))
            .Should()
            .ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task UpdateProduct_PictureUriValidationFailed()
    {
        var updateCommand = new UpdateProductCommand()
        {
            ProductId = Guid.NewGuid(),
            Name = "Name",
            AvailableStock = 1,
            PricePerUnit = 1,
            UnitId = SeedData.Unit1Id1,
            Description = "desc",
            PictureUri = string.Empty,
            PictureFilename = "filename",
            MaxStockThreshold = 1,
            CatalogId = SeedData.Catalog1Id
        };

        await FluentActions
            .Invoking(() => SendAsync(updateCommand))
            .Should()
            .ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task UpdateProduct_NotFoundFailed()
    {
        var updateCommand = new UpdateProductCommand()
        {
            ProductId = Guid.NewGuid(),
            Name = "product",
            AvailableStock = 1,
            CatalogId = SeedData.Catalog1Id,
            PricePerUnit = 20,
            MaxStockThreshold = 10,
            Description = "desc",
            PictureUri = "uri1",
            PictureFilename = "filename2",
            UnitId = SeedData.Unit1Id1
        };

        await FluentActions
            .Invoking(() => SendAsync(updateCommand))
            .Should()
            .ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task UpdateProduct_Success()
    {
        var productId = await AddProduct();

        var updateCommand = new UpdateProductCommand()
        {
            ProductId = productId,
            Name = "Updated Product",
            AvailableStock = 1,
            CatalogId = SeedData.Catalog1Id,
            PricePerUnit = 20,
            MaxStockThreshold = 10,
            Description = "desc",
            PictureUri = "uri1",
            PictureFilename = "filename2",
            UnitId = SeedData.Unit1Id1
        };

        await SendAsync(updateCommand);

        var product = await FindAsync<Domain.Product.Product>(productId);

        product.Should().NotBeNull();
        product?.Name.Should().Be("Updated Product");
        product?.PricePerUnit.Should().Be(20);
        product?.MaxStockThreshold.Should().Be(10);
    }

    private Task<Guid> AddProduct()
    {
        var command = new AddProductCommand()
        {
            Name = "Product 1",
            UnitId = SeedData.Unit1Id1,
            PricePerUnit = 1,
            AvailableStock = 1,
            Description = "Desc",
            PictureFilename = "filename",
            PictureUri = "pictureuri"
        };

        return SendAsync(command);
    }

}