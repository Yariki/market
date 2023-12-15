using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Domain.Exceptions;
public class BasketException : Exception
{
    public BasketException()
    {
    }

    public BasketException(string message)
        : base(message)
    {
    }

    public BasketException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
