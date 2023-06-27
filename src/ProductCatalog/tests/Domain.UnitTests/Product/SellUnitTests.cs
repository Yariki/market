using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using ProductCatalog.Domain.Product;

namespace ProductCatalog.Domain.UnitTests.Product;
public class SellUnitTests
{

    [Test]
    public void CreateSellUnit()
    {
        var sellUnit = SellUnit.CreateNew(Guid.NewGuid(), 1);

        sellUnit.UnitId.Should().NotBeEmpty();
        sellUnit.Scalar.Should().Be(1);
        sellUnit.Id.Should().NotBeEmpty();
    }


}
