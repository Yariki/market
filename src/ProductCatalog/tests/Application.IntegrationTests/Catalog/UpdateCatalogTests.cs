using FluentAssertions;
using FluentValidation;
using NUnit.Framework;
using ProductCatalog.Application.Catalogs.Commands.AddCatalog;
using ProductCatalog.Application.Catalogs.Commands.UpdateCatalog;

using static ProductCatalog.Application.IntegrationTests.AppicationTesting;

namespace ProductCatalog.Application.IntegrationTests.Catalog;

public class UpdateCatalogTests : ApplicationBaseFixture
{

    [Test]
    public async Task UpdateCommand_ValidationIdFailed()
    {
        var update = new UpdateCatalogCommand()
        {
            Id = Guid.Empty,
            Name = "Test",
            Description = "Test"
        };

        await FluentActions
            .Invoking(() => SendAsync(update))
            .Should()
            .ThrowAsync<ValidationException>();

    }

    [Test]
    public async Task UpdateCommand_ValidationNameFailed()
    {
        var update = new UpdateCatalogCommand()
        {
            Id = Guid.NewGuid(),
            Name = string.Empty,
            Description = string.Empty
        };

        await FluentActions
            .Invoking(() => SendAsync(update))
            .Should()
            .ThrowAsync<ValidationException>();

    }


    [Test]
    public async Task UpdateCatalog_Success()
    {
        var id = await AddCatalog("Catalog1");

        id.Should().NotBeEmpty();
        id.Should().NotBe(Guid.Empty);

        var update = new UpdateCatalogCommand()
        {
            Id = id,
            Name = "Catalog2",
            Description = "Desc"
        };

        var catalogId = await SendAsync(update);

        var updatedCatalog = await FindAsync<Domain.Catalogs.Catalog>(catalogId);

        updatedCatalog.Should().NotBeNull();
        updatedCatalog.Name.Should().Be("Catalog2");
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
