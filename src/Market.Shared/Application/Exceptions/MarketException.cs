using System.Runtime.Serialization;

namespace Market.Shared.Application.Exceptions;

public class MarketException : Exception
{
    public MarketException()
    {
    }

    protected MarketException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public MarketException(string? message) : base(message)
    {
    }

    public MarketException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}