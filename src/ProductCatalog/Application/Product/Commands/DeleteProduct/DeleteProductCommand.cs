﻿using Market.Shared.Application.Exceptions;
using Market.Shared.Infrastructure.Common.Extensions;
using MediatR;
using Microsoft.Extensions.DependencyInjection.Common.Services;

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