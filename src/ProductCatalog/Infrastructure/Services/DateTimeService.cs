using Market.Shared.Application.Interfaces;

namespace ProductCatalog.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
