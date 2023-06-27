using FluentAssertions;
using NUnit.Framework;
using ProductCatalog.Domain.Product;

namespace ProductCatalog.Domain.UnitTests;

public class UnitTests
{
    [Test]
    public void CreateUnit()
    {
        var unit = Unit.CreateUnit("kg", description: "Kilogram");

        unit.Abbriviation.Should().Be("kg");
        unit.Description.Should().Be("Kilogram");
        unit.Id.Should().NotBeEmpty();
    }

}
