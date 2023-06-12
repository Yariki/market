using Market.Shared.Application.Mappings;
using CatalogEntity = ProductCatalog.Domain.Catalog.Catalog;

namespace Microsoft.Extensions.DependencyInjection.Catalog.Models;

public class CatalogDto : IMapFrom<CatalogEntity>
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }
}