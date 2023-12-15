using Basket.Application.Common.Mappings;
using Basket.Domain.Entities;

namespace Basket.Application.SampleItems.Queries.GetSampleItemsWithPagination;

public class SampleItemBriefDto : IMapFrom<SampleItem>
{
    public int Id { get; init; }

    public string? Title { get; init; }

    public bool Done { get; init; }
}
