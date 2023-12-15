using Market.Shared.Application.Exceptions;
using Market.Shared.Application.Extensions;
using MediatR;
using ProductCatalog.Application.Common.Services;


namespace Microsoft.Extensions.DependencyInjection.Product.Commands.DeleteProduct;

public class DeleteProductCommand : IRequest
{
    public Guid ProductId { get; set; }
    
}

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IProductCatalogDbContext _productCatalogDbContext;

    public DeleteProductCommandHandler(IProductCatalogDbContext productCatalogDbContext)
    {
        _productCatalogDbContext = productCatalogDbContext;
    }

    public Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = _productCatalogDbContext.Products.Find(request.ProductId);

        if (product.IsNull())
        {
            throw new NotFoundException($"Product with id {request.ProductId} not found");
        }

        _productCatalogDbContext.Products.Remove(product);

        return _productCatalogDbContext.SaveChangesAsync(cancellationToken);    
    }
}