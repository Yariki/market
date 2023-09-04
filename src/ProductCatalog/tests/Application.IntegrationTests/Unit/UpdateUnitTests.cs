using FluentAssertions;
using FluentValidation;
using Market.Shared.Application.Exceptions;
using Market.Shared.Integration.Tests;
using Microsoft.Extensions.DependencyInjection.Unit.Commands.UpdateUnit;
using NUnit.Framework;
using ProductCatalog.Application.UnitEntity.Commands.AddUnit;
using static ProductCatalog.Application.IntegrationTests.AppicationTesting;

namespace ProductCatalog.Application.IntegrationTests.Unit;


public class UpdateUnitTests : BaseTestFixture
{
    [Test]
    public async Task UpdateUnit_IdValidationFailed()
    {
        var updateCmd = new UpdateUnitCommand()
        {
            UnitId = Guid.Empty,
            Abbriviation = string.Empty,
            Description = "Desc"
        };

        await FluentActions
            .Invoking(() => SendAsync(updateCmd))
            .Should()
            .ThrowAsync<FluentValidation.ValidationException>();
    }

    [Test]
    public async Task UpdateUnit_AbbriviationValidationFailed()
    {
        var updateCmd = new UpdateUnitCommand()
        {
            UnitId = Guid.NewGuid(),
            Abbriviation = string.Empty,
            Description = "Desc"
        };

        await FluentActions
            .Invoking(() => SendAsync(updateCmd))
            .Should()
            .ThrowAsync<FluentValidation.ValidationException>();
    }

    [Test]
    public async Task UpdateUnit_DesciptionValidationFailed()
    {
        var updateCmd = new UpdateUnitCommand()
        {
            UnitId = Guid.NewGuid(),
            Abbriviation = "cm",
            Description = new string('.', 256)
        };

        await FluentActions
            .Invoking(() => SendAsync(updateCmd))
            .Should()
            .ThrowAsync<FluentValidation.ValidationException>();
    }


    [Test]
    public async Task UpdateUnit_FailedNotFound()
    {
        var updateCmd = new UpdateUnitCommand()
        {
            UnitId = Guid.NewGuid(),
            Abbriviation = "cm",
            Description = "cm"
        };

        await FluentActions
            .Invoking(() => SendAsync(updateCmd))
            .Should()
            .ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task UpdateUnit_Success()
    {
        var unitId = await CreateUnitAsync("cm", "Centimeter");

        var updateCmd = new UpdateUnitCommand()
        {
            UnitId = unitId,
            Abbriviation = "m",
            Description = "Meter"
        };

        await SendAsync(updateCmd);

        var unit = await FindAsync<Domain.Product.Unit>(unitId);

        unit.Should().NotBeNull();
        unit.Abbriviation.Should().Be(updateCmd.Abbriviation);
        unit.Description.Should().Be(updateCmd.Description);
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
