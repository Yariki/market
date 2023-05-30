using Market.Shared.Application.Interfaces;

namespace Market.Shared.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
