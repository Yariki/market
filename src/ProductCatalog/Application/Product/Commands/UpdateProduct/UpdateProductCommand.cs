using Market.Shared.Application.Exceptions;
using Market.Shared.Infrastructure.Common.Extensions;
using MediatR;
using Microsoft.Extensions.DependencyInjection.Common.Services;

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
        
        return  _productCatalogDbContext.SaveChangesAsync(cancellationToken);
    }
}