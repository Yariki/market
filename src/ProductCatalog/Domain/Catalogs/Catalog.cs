using ProductCatalog.Domain.Common;

namespace ProductCatalog.Domain.Catalogs;

public class Catalog : BaseIdEntity
{
    public string Name { get; set; }

    public string Description { get; set; }
}