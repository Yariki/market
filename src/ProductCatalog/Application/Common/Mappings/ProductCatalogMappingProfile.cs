using System.Reflection;
using Market.Shared.Application.Mappings;

namespace ProductCatalog.Application;

public class ProductCatalogMappingProfile : MappingProfile
{
    public ProductCatalogMappingProfile()
    {
        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
