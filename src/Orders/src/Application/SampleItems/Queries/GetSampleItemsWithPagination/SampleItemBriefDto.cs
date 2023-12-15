using Orders.Application.Common.Mappings;
using Orders.Domain.Entities;

namespace Orders.Application.SampleItems.Queries.GetSampleItemsWithPagination;

public class SampleItemBriefDto : IMapFrom<SampleItem>
{
    public int Id { get; init; }

    public string? Title { get; init; }

    public bool Done { get; init; }
}
