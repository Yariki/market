using Market.Shared.Application.Interfaces;

namespace Market.Shared.Services.Services;

public class DataTimeService : IDateTime
{
    public DateTime Now  => DateTime.Now;
}