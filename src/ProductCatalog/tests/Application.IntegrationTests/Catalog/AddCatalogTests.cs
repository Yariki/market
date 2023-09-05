using FluentAssertions;
using FluentValidation;
using Market.Shared.Integration.Tests;
using NUnit.Framework;
using ProductCatalog.Application.Catalogs.Commands.AddCatalog;

using static ProductCatalog.Application.IntegrationTests.AppicationTesting;

namespace ProductCatalog.Application.IntegrationTests.Catalog;

public class AddCatalogTests : BaseTestFixture
{
    [Test]
    public async Task AddCatalog_ValidationFailed()
    {
        var addCatalog = new AddCatalogCommand();

        await FluentActions
            .Invoking(() => SendAsync(addCatalog))
            .Should()
            .ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task AddCatalog_Success()
    {
        var addCommand = new AddCatalogCommand() { Name = "Catalog1", Description = " Desc" };

        var catalogId = await SendAsync(addCommand);

        var catalog = await FindAsync<Domain.Catalogs.Catalog>(catalogId);

        catalog.Should().NotBeNull();
        catalog.Name.Should().Be("Catalog1");
    }

}