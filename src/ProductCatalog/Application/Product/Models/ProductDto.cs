using Market.Shared.Application.Mappings;

namespace ProductCatalog.Application.Product.Models;

public class ProductDto : IMapFrom<ProductCatalog.Domain.Product.Product>
{
    
    public Guid Id { get; set; }
    
    public string Name { get; set; }

    public string Description { get; set; }

    public decimal? PricePerUnit { get; set; }
    
    public decimal AvailableStock { get; set; }

    public string PictureUri { get; set; }

    public string PictureFilename { get; set; }

    public decimal MaxStockThreshold { get; set; }

}