using FluentAssertions;
using FluentValidation;
using Market.Shared.Integration.Tests;
using NUnit.Framework;
using ProductCatalog.Application.Units.Commands.AddUnit;

using static ProductCatalog.Application.IntegrationTests.AppicationTesting;

namespace ProductCatalog.Application.IntegrationTests.Unit;

public class AddUnitTests : BaseTestFixture
{
    [Test]
    public async Task AddUnit_AbbriviationValidationFailed()
    {
        var addUnit = new AddUnitCommand()
        {
            Abbriviation = string.Empty,
            Description = "Desc"
        };

        await FluentActions
            .Invoking(() => SendAsync(addUnit))
            .Should()
            .ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task AddUnit_DescriptionValidationFailed()
    {
        var desc = new string('.', 257);

        var addUnit = new AddUnitCommand()
        {
            Abbriviation = "Abbr",
            Description = desc
        };

        await FluentActions
            .Invoking(() => SendAsync(addUnit))
            .Should()
            .ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task AddUnit_Success()
    {
        var addUnit = new AddUnitCommand()
        {
            Abbriviation = "cm",
            Description = "cm"
        };

        var unitId = await SendAsync(addUnit);

        var unit = await FindAsync<Domain.Product.Unit>(unitId);

        unit.Should().NotBeNull();
        unit.Abbriviation.Should().Be("cm");
    }



}