using Market.Shared.Application.Exceptions;
using Market.Shared.Infrastructure.Common.Extensions;
using MediatR;
using ProductCatalog.Application.Common.Services;
using ProductCatalog.Application.Product.Models;

namespace Microsoft.Extensions.DependencyInjection.Product.Commands.UpdateProduct;

public class UpdateProductCommand : IRequest
{
    public Guid ProductId { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public decimal? PricePerUnit { get; set; }
    
    public Guid? CatalogId { get; set; }
    
    public decimal? AvailableStock { get; set; }
    
    public string PictureUri { get; set; }
    
    public string PictureFilename { get; set; }
    
    public decimal? MaxStockThreshold { get; set; }

    public SellUnitDto[] SellUnits { get; set; }    
    
}

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IProductCatalogDbContext _productCatalogDbContext;
    
    public UpdateProductCommandHandler(IProductCatalogDbContext productCatalogDbContext)
    {
        _productCatalogDbContext = productCatalogDbContext;
    }    
    
    public Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = _productCatalogDbContext.Products.Find(request.ProductId);

        if (product.IsNull())
        {
            throw new NotFoundException($"Product with id {request.ProductId} not found");
        }

        product.Name = request.Name;
        request.Description.IfNotNullSet(desc => product.Description = desc);
        request.PricePerUnit.IfNotNullSet(price => product.SetPrice(price.Value));
        request.CatalogId.IfNotNullSet(catalogId => product.CatalogId = catalogId.Value);        
        request.AvailableStock.IfNotNullSet(availableStock => product.AddStock(availableStock.Value));
        request.PictureUri.IfNotNullSet(pictureUri => product.PictureUri = pictureUri);
        request.PictureFilename.IfNotNullSet(pictureFilename => product.PictureFilename = pictureFilename);
        product.MaxStockThreshold.IfNotNullSet(m => product.MaxStockThreshold = m);

        UpdateSellUnits(product, request.SellUnits);
        
        return  _productCatalogDbContext.SaveChangesAsync(cancellationToken);
    }

    private void UpdateSellUnits( ProductCatalog.Domain.Product.Product product, SellUnitDto[] sellUnits)
    {
        var existingSellUnits = product.SellUnits.ToList();
        var newSellUnits = sellUnits.Where(sellUnit => existingSellUnits.All(existingSellUnit => existingSellUnit.Id != sellUnit.Id)).ToList();
        var removedSellUnits = existingSellUnits.Where(existingSellUnit => sellUnits.All(sellUnit => sellUnit.Id != existingSellUnit.Id)).ToList();

        foreach (var sellUnit in newSellUnits)
        {
            product.AddSellUnit(sellUnit.UnitId, sellUnit.Scalar);
        }

        foreach (var sellUnit in removedSellUnits)
        {
            product.RemoveSellUnit(sellUnit.Id);
        }
    }

}