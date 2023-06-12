using System.Net;
using Market.Shared.Application.Exceptions;
using Market.Shared.Infrastructure.Common.Extensions;
using MediatR;
using Microsoft.Extensions.DependencyInjection.Common.Services;

namespace Microsoft.Extensions.DependencyInjection.Product.Commands.AddSellUnit;

public class AddSellUnitToProductCommand : IRequest
{
    public Guid ProductId { get; set; }
    
    public IEnumerable<SellUnitDto> SellUnit { get; set; }

}

public class AddSellUnitToProductCommandHandler : IRequestHandler<AddSellUnitToProductCommand>
{

    private readonly IProductCatalogDbContext _productCatalogDbContext;

    public AddSellUnitToProductCommandHandler(IProductCatalogDbContext productCatalogDbContext)
    {
        _productCatalogDbContext = productCatalogDbContext;
    }

    public async Task Handle(AddSellUnitToProductCommand request,
        CancellationToken cancellationToken)
    {
        var product = await _productCatalogDbContext.Products.FindAsync(request.ProductId);

        if (product.IsNull())
        {
            throw new NotFoundException($"Product with id {request.ProductId} not found");
        }

        foreach (var sellUnitDto in request.SellUnit)
        {
            product.AddSellUnit(sellUnitDto.UnitId, sellUnitDto.Scalar);
        }


        await _productCatalogDbContext.SaveChangesAsync(cancellationToken);
        
    }
}
