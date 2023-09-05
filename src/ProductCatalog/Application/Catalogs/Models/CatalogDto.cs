using Market.Shared.Application.Mappings;
using ProductCatalog.Domain.Catalogs;

namespace ProductCatalog.Application.Catalogs.Models;

public class CatalogDto : IMapFrom<Catalog>
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }
}