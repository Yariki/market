using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Market.Shared.Application.Interfaces;
using Market.Shared.Infrastructure.Common.Extensions;
using MediatR;
using ProductCatalog.Application.Common.Services;
using ProductCatalog.Application.Product.Models;

namespace ProductCatalog.Application.Product.Commands.AddProduct;
public class AddProductCommand : IRequest<Guid>
{
    public string Name { get; set; }

    public Guid UnitId { get; set; }

    public decimal PricePerUnit { get; set; }

    public decimal AvailableStock { get; set; }

    public string PictureUri { get; set; }

    public string PictureFilename { get; set; }

    public string Description { get; set; }

    public decimal MaxStockThreshold { get; set; }

    public Guid? CatalogId { get; set; }
    
    public SellUnitDto[] SellUnits { get; set; }

}

public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Guid>
{
    private readonly IProductCatalogDbContext _productCatalogDbContext;
    private readonly ICurrentUserService _currentUserService;


    public AddProductCommandHandler(IProductCatalogDbContext productCatalogDbContext, 
        ICurrentUserService currentUserService)
    {
        _productCatalogDbContext = productCatalogDbContext;
        _currentUserService = currentUserService;
    }

    public async Task<Guid> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        var product = new Domain.Product.Product(request.Name, 
            userId, 
            request.UnitId, 
            request.PricePerUnit, 
            request.CatalogId, 
            request.AvailableStock, 
            request.PictureUri, 
            request.PictureFilename, 
            request.Description)
        {
            MaxStockThreshold = request.MaxStockThreshold
        };
       
        await _productCatalogDbContext.Products.AddAsync(product, cancellationToken);
        await _productCatalogDbContext.SaveChangesAsync(cancellationToken);

        await AddSellUnits(product, request.SellUnits, cancellationToken);
        
        return product.Id;
    }

    private async Task AddSellUnits(Domain.Product.Product product, SellUnitDto[] sellUnits, CancellationToken cancellationToken)
    {
        if (sellUnits.IsNull() || !sellUnits.Any())
        {
            return;
        }

        foreach (var sellUnit in sellUnits)
        {
            product.AddSellUnit(sellUnit.UnitId, sellUnit.Scalar);
        }

        await _productCatalogDbContext.SaveChangesAsync(cancellationToken);
    }
    
}





