using System.Runtime.Serialization;
using Market.Shared.Domain;

namespace Market.Shared.Application.Exceptions;

public class ForbiddenAccessException : MarketException
{
    public ForbiddenAccessException()
    {
    }

    protected ForbiddenAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public ForbiddenAccessException(string? message) : base(message)
    {
    }

    public ForbiddenAccessException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}