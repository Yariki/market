using Basket.Application.Common.Interfaces;

namespace Basket.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
