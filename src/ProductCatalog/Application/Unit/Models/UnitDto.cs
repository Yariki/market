using Market.Shared.Application.Mappings;
using UnitObject = ProductCatalog.Domain.Product.Unit;

namespace ProductCatalog.Application;

public class UnitDto : IMapFrom<UnitObject>
{
    public Guid Id { get; set; }

    public string Abbriviation { get; set; }
    
    public string  Description { get; set; }
}
