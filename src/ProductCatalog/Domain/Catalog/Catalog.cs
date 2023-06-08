using Market.Shared.Domain;
using ProductCatalog.Domain.Common;

namespace ProductCatalog.Domain.Catalog;

public class Catalog : BaseIdEntity
{
    public string Name { get; set; }

    public string Description { get; set; }
}