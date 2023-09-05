using FluentAssertions;
using Market.Shared.Application.Exceptions;
using Market.Shared.Integration.Tests;
using NUnit.Framework;
using ProductCatalog.Application.Catalogs.Commands.AddCatalog;
using ProductCatalog.Application.Catalogs.Commands.DeleteCatalog;
using static ProductCatalog.Application.IntegrationTests.AppicationTesting;

namespace ProductCatalog.Application.IntegrationTests.Catalog;

public class DeleteCatalogTests : BaseTestFixture
{

    [Test]
    public async Task DeleteCatalog_ValidationIdFailed()
    {
        var deleteCommand = new DeleteCatalogCommand()
        {
            Id = Guid.Empty
        };

        await FluentActions
            .Invoking(() => SendAsync(deleteCommand))
            .Should()
            .ThrowAsync<FluentValidation.ValidationException>();
    }


    [Test]
    public async Task DeleteCatalog_FailedNotFound()
    {
        var deleteCommand = new DeleteCatalogCommand()
        {
            Id = Guid.NewGuid()
        };


        await FluentActions
            .Invoking(() => SendAsync(deleteCommand))
            .Should()
            .ThrowAsync<NotFoundException>();

    }


    [Test]
    public async Task DeleteCatalog_Success()
    {
        var id = await AddCatalog("Catalog1");

        id.Should().NotBeEmpty();
        id.Should().NotBe(Guid.Empty);

        var deleteCommand = new DeleteCatalogCommand()
        {
            Id = id
        };

        var result = await SendAsync(deleteCommand);

        result.Should().BeTrue();

        var catalog = await FindAsync<Domain.Catalogs.Catalog>(id);

        catalog.Should().BeNull();
    }

    private async Task<Guid> AddCatalog(string name)
    {
        var add = new AddCatalogCommand()
        {
            Name = name,
            Description = "Test"
        };

        var id = await SendAsync(add);
        return id;
    }

}