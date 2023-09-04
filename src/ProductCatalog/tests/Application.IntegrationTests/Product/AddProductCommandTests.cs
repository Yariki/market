using FluentAssertions;
using FluentValidation;
using Market.Shared.Integration.Tests;
using NUnit.Framework;
using ProductCatalog.Application.IntegrationTests.Data;
using ProductCatalog.Application.Product.Commands.AddProduct;
using ProductCatalog.Application.UnitEntity.Commands.AddUnit;

using static ProductCatalog.Application.IntegrationTests.AppicationTesting;

namespace ProductCatalog.Application.IntegrationTests;

public class AddProductCommandTests : BaseTestFixture
{
    [Test]
    public async Task AddProduct_NameValidationFailed()
    {
        var command = new AddProductCommand()
        {
            Name = string.Empty,
            UnitId = SeedData.Unit1Id1,
            PricePerUnit = 1,
            AvailableStock = 1
        };

        await FluentActions
            .Invoking(() => SendAsync(command))
            .Should()
            .ThrowAsync<ValidationException>()
            .Where(x => x.Errors.Any(e => e.PropertyName == nameof(command.Name)));
    }

    [Test]
    public async Task AddProduct_UnitIdValidationFailed()
    {
        var command = new AddProductCommand()
        {
            Name = "Product 1",
            UnitId = Guid.Empty,
            PricePerUnit = 1,
            AvailableStock = 1
        };

        await FluentActions
            .Invoking(() => SendAsync(command))
            .Should()
            .ThrowAsync<ValidationException>()
            .Where(x => x.Errors.Any(e => e.PropertyName == nameof(command.UnitId)));
    }

    [Test]
    public async Task AddProduct_PricePerUnitValidationFailed()
    {
        var command = new AddProductCommand()
        {
            Name = "Product 1",
            UnitId = SeedData.Unit1Id1,
            PricePerUnit = 0,
            AvailableStock = 1
        };

        await FluentActions
            .Invoking(() => SendAsync(command))
            .Should()
            .ThrowAsync<ValidationException>()
            .Where(x => x.Errors.Any(e => e.PropertyName == nameof(command.PricePerUnit)));
    }

    [Test]
    public async Task AddProduct_AvailableStockValidationFailed()
    {
        var command = new AddProductCommand()
        {
            Name = "Product 1",
            UnitId = SeedData.Unit1Id1,
            PricePerUnit = 1,
            AvailableStock = 0
        };

        await FluentActions
            .Invoking(() => SendAsync(command))
            .Should()
            .ThrowAsync<ValidationException>()
            .Where(x => x.Errors.Any(e => e.PropertyName == nameof(command.AvailableStock)));
    }
    
    [Test]
    public async Task AddProduct_DescriptionValidationFailed()
    {
        var command = new AddProductCommand()
        {
            Name = "Product 1",
            UnitId = SeedData.Unit1Id1,
            PricePerUnit = 1,
            AvailableStock = 0,
            Description = string.Empty,
            PictureFilename = "filename",
            PictureUri = "uri"
        };

        await FluentActions
            .Invoking(() => SendAsync(command))
            .Should()
            .ThrowAsync<ValidationException>()
            .Where(x => x.Errors.Any(e => e.PropertyName == nameof(command.Description)));
    }
    
    [Test]
    public async Task AddProduct_PictureFilenameValidationFailed()
    {
        var command = new AddProductCommand()
        {
            Name = "Product 1",
            UnitId = SeedData.Unit1Id1,
            PricePerUnit = 1,
            AvailableStock = 0,
            Description = "desc",
            PictureFilename = string.Empty,
            PictureUri = "uri"
        };

        await FluentActions
            .Invoking(() => SendAsync(command))
            .Should()
            .ThrowAsync<ValidationException>()
            .Where(x => x.Errors.Any(e => e.PropertyName == nameof(command.PictureFilename)));
    }
    
    [Test]
    public async Task AddProduct_PictureUriValidationFailed()
    {
        var command = new AddProductCommand()
        {
            Name = "Product 1",
            UnitId = SeedData.Unit1Id1,
            PricePerUnit = 1,
            AvailableStock = 0,
            Description = "desc",
            PictureFilename = "filename",
            PictureUri = string.Empty
        };

        await FluentActions
            .Invoking(() => SendAsync(command))
            .Should()
            .ThrowAsync<ValidationException>()
            .Where(x => x.Errors.Any(e => e.PropertyName == nameof(command.PictureUri)));
    }

    [Test]
    public async Task AddProduct_Success()
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

        var productId = await SendAsync(command);

        var product = await FindAsync<Domain.Product.Product>(productId);

        product.Should().NotBeNull();
        product?.Name.Should().Be("Product 1");
        product?.PricePerUnit.Should().Be(1);
        product?.AvailableStock.Should().Be(1);
    }

}
