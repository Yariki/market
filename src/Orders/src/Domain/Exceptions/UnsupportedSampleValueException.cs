namespace Orders.Domain.Exceptions;

public class UnsupportedSampleValueException : Exception
{
    public UnsupportedSampleValueException(string code)
        : base($"Sample value \"{code}\" is unsupported.")
    {
    }
}
