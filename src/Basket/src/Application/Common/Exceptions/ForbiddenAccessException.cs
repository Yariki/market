using Basket.Domain.Exceptions;

namespace Basket.Application.Common.Exceptions;

public class ForbiddenAccessException : BasketException
{
    public ForbiddenAccessException() : base() { }
}
