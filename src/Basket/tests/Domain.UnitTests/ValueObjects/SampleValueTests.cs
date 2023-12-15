using Basket.Domain.Exceptions;
using Basket.Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;

namespace Basket.Domain.UnitTests.ValueObjects;

public class SampleValueTests
{
    [Test]
    public void ShouldReturnCorrectColourCode()
    {
        var code = "#FFFFFF";

        var colour = SampleValue.From(code);

        colour.Code.Should().Be(code);
    }

    [Test]
    public void ToStringReturnsCode()
    {
        var colour = SampleValue.White;

        colour.ToString().Should().Be(colour.Code);
    }

    [Test]
    public void ShouldPerformImplicitConversionToColourCodeString()
    {
        string code = SampleValue.White;

        code.Should().Be("#FFFFFF");
    }

    [Test]
    public void ShouldPerformExplicitConversionGivenSupportedColourCode()
    {
        var colour = (SampleValue)"#FFFFFF";

        colour.Should().Be(SampleValue.White);
    }

    [Test]
    public void ShouldThrowUnsupportedColourExceptionGivenNotSupportedColourCode()
    {
        FluentActions.Invoking(() => SampleValue.From("##FF33CC"))
            .Should().Throw<UnsupportedSampleValueException>();
    }
}
