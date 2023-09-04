using FluentAssertions;
using FluentValidation;
using Market.Shared.Application.Exceptions;
using Market.Shared.Integration.Tests;
using Microsoft.Extensions.DependencyInjection.Unit.Commands.UpdateUnit;
using NUnit.Framework;
using Org.BouncyCastle.Asn1;
using ProductCatalog.Application.UnitEntity.Commands.AddUnit;
using static ProductCatalog.Application.IntegrationTests.AppicationTesting;

namespace ProductCatalog.Application.IntegrationTests;

public class GetUnitsTests : BaseTestFixture
{

    [Test]
    public async Task GsetUnits_ReturnEmptySuccess()
    {
        var query = new GetUnitQuery();

        var units = await SendAsync(query);

        units.Should().BeEmpty();
    }

    [Test]
    public async Task GetUnits_ReturnSuccess()
    {
        await CreateUnitAsync("cm", "Centimeter");
        await CreateUnitAsync("M", "meter");

        var query = new GetUnitQuery();

        var units = await SendAsync(query);

        units.Should().NotBeEmpty();
        units.Count().Should().Be(2);
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
