using Orders.Application.Common.Interfaces;

namespace Orders.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
