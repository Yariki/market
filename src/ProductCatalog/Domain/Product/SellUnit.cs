using Market.Shared.Domain;
using ProductCatalog.Domain.Common;

namespace ProductCatalog.Domain.Product;

public class SellUnit : BaseIdEntity
{

    private decimal _scalar;

    public SellUnit()
    {
        
    }
    
    public SellUnit(Guid unitId, decimal scalar)
    {
        UnitId = unitId;
        _scalar = scalar;
    }
    
    public Guid UnitId { get; set; }
    
    public virtual Unit Unit { get; set; }
    
    public decimal Scalar { get => _scalar; }

    public decimal GetPrice(Product product)
    {
        return Scalar * product.PricePerUnit;
    }
    
    public static SellUnit CreateNew(Guid unitId, decimal scalar)
    {
        return new SellUnit(unitId, scalar) { Id = Guid.NewGuid()};
    }
}