﻿using System.Runtime.Serialization;

namespace Market.Shared.Domain;

public class MarketException : Exception
{
    public MarketException()
    {
    }

    public MarketException(string? message) : base(message)
    {
    }

    public MarketException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}