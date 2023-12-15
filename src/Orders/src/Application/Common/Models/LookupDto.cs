using Orders.Application.Common.Mappings;
using Orders.Domain.Entities;

namespace Orders.Application.Common.Models;

// Note: This is currently just used to demonstrate applying multiple IMapFrom attributes.
public class LookupDto : IMapFrom<SampleItem>
{
    public int Id { get; init; }

    public string? Title { get; init; }
}
