using Market.Shared.Domain;
using ProductCatalog.Domain.Common;

namespace ProductCatalog.Domain.Product;

public class Product : BaseIdEntity, IAggregatorRoot, IAuditableEntity
{
    private decimal _pricePerUnit;
    private decimal _availableStock;
    private string _userId;

    private readonly List<SellUnit> _sellUnits;
    
    public Product()
    {
        _sellUnits = new List<SellUnit>();
    }

    public Product(string name, string userId, Guid unitId, 
        decimal pricePerUnit, Guid? catalogId, 
        decimal availableStock, string pictureUri, 
        string pictureFilename, string description)
        :this()
    {
        Name = name;
        _userId = userId;
        UnitId = unitId;
        _pricePerUnit = pricePerUnit;
        CatalogId = catalogId;
        _availableStock = availableStock;
        PictureUri = pictureUri;
        PictureFilename = pictureFilename;
        Description = description;
    }
    
    public string Name { get; set; }
    
    public string UserId { get => _userId; }
    
    public Guid UnitId { get; set; }
    
    public virtual Unit Unit { get; set; }
    
    public decimal PricePerUnit { get => _pricePerUnit; }

    public IReadOnlyCollection<SellUnit> SellUnits => _sellUnits;

    public Guid? CatalogId { get; set; }
    
    public virtual Catalog.Catalog Catalog { get; set; }
    
    public decimal AvailableStock { get => _availableStock; }
    
    public string PictureUri { get; set; }
    
    public string PictureFilename { get; set; }
    
    public string Description { get; set; }
    
    public decimal MaxStockThreshold { get; set; }

    public void AddSellUnit(Guid unitId, decimal scalar)
    {
        if(_sellUnits.Any(x => x.Scalar == scalar))
        {
            throw new MarketException("Sell unit already exists");
        }
        
        _sellUnits.Add(SellUnit.CreateNew(unitId, scalar));
    }
    
    public void RemoveSellUnit(Guid id)
    {
        var sellUnit = _sellUnits.FirstOrDefault(x => x.Id == id);
        if(sellUnit == null)
        {
            throw new MarketException("Sell unit not found");
        }

        _sellUnits.Remove(sellUnit);
    }
    
    
    public bool IsEnough(decimal quantity)
    {
        return _availableStock >= quantity;
    }
    
    public void AddStock(decimal quantity)
    {
        if(_availableStock + quantity > MaxStockThreshold)
        {
            throw new MarketException("Exceeding maximum stock threshold");
        }

        _availableStock += quantity;
    }
    
    public void RemoveStock(decimal quantity)
    {
        if(!IsEnough(quantity))
        {
            throw new MarketException("Not enough stock");
        }

        _availableStock -= quantity;
    }

    public void SetUser(string userId)
    {
        _userId = userId;
    }
    
    public void SetPrice(decimal price)
    {
        _pricePerUnit = price;
    }

    public DateTime Created { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
}
