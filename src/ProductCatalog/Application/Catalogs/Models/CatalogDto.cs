using Market.Shared.Application.Mappings;
using CatalogEntity = ProductCatalog.Domain.Catalogs.Catalog;

namespace ProductCatalog.Application.Catalogs.Models;

public class CatalogDto : IMapFrom<CatalogEntity>
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }
}