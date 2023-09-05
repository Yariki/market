using FluentAssertions;
using Market.Shared.Application.Exceptions;
using NUnit.Framework;
using ProductCatalog.Application.Units.Commands.AddUnit;
using ProductCatalog.Application.Units.Commands.DeleteUnit;
using static ProductCatalog.Application.IntegrationTests.AppicationTesting;

namespace ProductCatalog.Application.IntegrationTests;

public class DeleteUnitTests : ApplicationBaseFixture
{
    [Test]
    public async Task DeleteUnit_IdValidationFailed()
    {
        var deleteCmd = new DeleteUnitCommand()
        {
            UnitId = Guid.Empty
        };

        await FluentActions
            .Invoking(() => SendAsync(deleteCmd))
            .Should()
            .ThrowAsync<FluentValidation.ValidationException>();
    }

    [Test]
    public async Task DeleteUnit_NotFound()
    {
        var deleteCmd = new DeleteUnitCommand()
        {
            UnitId = Guid.NewGuid()
        };

        await FluentActions
            .Invoking(() => SendAsync(deleteCmd))
            .Should()
            .ThrowAsync<NotFoundException>();
    }


    [Test]
    public async Task DeleteUnit_Success()
    {
        var unitId = await CreateUnitAsync("cm", "Centimeter");

        var deleteCmd = new DeleteUnitCommand()
        {
            UnitId = unitId
        };

        await SendAsync(deleteCmd);

        var unit = await FindAsync<Domain.Product.Unit>(unitId);

        unit.Should().BeNull();
    }

    private async Task<Guid> CreateUnitAsync(string v1, string v2)
    {
        var add = new AddUnitCommand()
        {
            Abbriviation = v1,
            Description = v2
        };

        var unitId = await SendAsync(add);

        return unitId;
    }

}
