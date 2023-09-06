using FluentAssertions;
using Market.Shared.Application.Exceptions;
using Microsoft.Extensions.DependencyInjection.Product.Commands.DeleteProduct;
using NUnit.Framework;
using ProductCatalog.Application.IntegrationTests.Data;
using ProductCatalog.Application.Product.Commands.AddProduct;

using static ProductCatalog.Application.IntegrationTests.AppicationTesting;
using ValidationException = FluentValidation.ValidationException;

namespace ProductCatalog.Application.IntegrationTests;

public class DeleteProductCommandTests : ApplicationBaseFixture
{

    [Test]
    public async Task DeleteProduct_ProductIdValidationFailed()
    {
        var deleteCmd = new DeleteProductCommand();
        
        await FluentActions
            .Invoking(() => SendAsync(deleteCmd))
            .Should()
            .ThrowAsync<ValidationException>();
    }
    
    [Test]
    public async Task DeleteProduct_NotFound()
    {
        var deleteCmd = new DeleteProductCommand()
        {
            ProductId = Guid.NewGuid()
        };
        
        await FluentActions
            .Invoking(() => SendAsync(deleteCmd))
            .Should()
            .ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task DeleteProduct_Success()
    {
        var productId = await AddProduct();

        productId.Should().NotBeEmpty();
        productId.Should().NotBe(Guid.Empty);

        var deleteProductCommand = new DeleteProductCommand() { ProductId = productId };

        await SendAsync(deleteProductCommand);

        var product = await FindAsync<Domain.Product.Product>(productId);

        product.Should().BeNull();
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