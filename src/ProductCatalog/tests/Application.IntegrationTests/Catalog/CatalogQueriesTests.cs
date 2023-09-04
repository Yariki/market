using FluentAssertions;
using Market.Shared.Integration.Tests;
using NUnit.Framework;
using ProductCatalog.Application.Catalog.Commands.AddCatalog;
using ProductCatalog.Application.Catalog.Queries.GetCatalogs;
using static ProductCatalog.Application.IntegrationTests.AppicationTesting;

namespace ProductCatalog.Application.IntegrationTests;

public class CatalogQueriesTests : BaseTestFixture
{

    [Test]
    public async Task GetCatalog_Success()
    {
        await AddCatalog("Catalog1");
        await AddCatalog("Catalog2");
        await AddCatalog("Catalog3");

        var query = new GetCatalogsQuery();

        var result = await SendAsync(query);

        result.Should().NotBeNull();
        result.Count().Should().Be(3);
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
