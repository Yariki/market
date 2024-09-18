namespace Orders.Domain.ValueObjects;

public class SampleValue : ValueObject
{
    static SampleValue()
    {
    }

    private SampleValue()
    {
    }

    private SampleValue(string code)
    {
        Code = code;
    }

    public static SampleValue From(string code)
    {
        var colour = new SampleValue { Code = code };

        if (!SupportedColours.Contains(colour))
        {
            throw new UnsupportedSampleValueException(code);
        }

        return colour;
    }

    public static SampleValue White => new("#FFFFFF");

    public static SampleValue Red => new("#FF5733");

    public static SampleValue Orange => new("#FFC300");

    public static SampleValue Yellow => new("#FFFF66");

    public static SampleValue Green => new("#CCFF99");

    public static SampleValue Blue => new("#6666FF");

    public static SampleValue Purple => new("#9966CC");

    public static SampleValue Grey => new("#999999");

    public string Code { get; private set; } = "#000000";

    public static implicit operator string(SampleValue colour)
    {
        return colour.ToString();
    }

    public static explicit operator SampleValue(string code)
    {
        return From(code);
    }

    public override string ToString()
    {
        return Code;
    }

    protected static IEnumerable<SampleValue> SupportedColours
    {
        get
        {
            yield return White;
            yield return Red;
            yield return Orange;
            yield return Yellow;
            yield return Green;
            yield return Blue;
            yield return Purple;
            yield return Grey;
        }
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Code;
    }
}
