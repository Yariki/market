using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Market.Shared.Application.Exceptions;
using Market.Shared.Infrastructure.Common.Extensions;
using MediatR;
using ProductCatalog.Application.Common.Services;

namespace ProductCatalog.Application.Catalogs.Commands.DeleteCatalog;
public class DeleteCatalogCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}

public class DeleteCatalogCommandHandler : IRequestHandler<DeleteCatalogCommand, bool>
{
    private readonly IProductCatalogDbContext _productCatalogDbContext;

    public DeleteCatalogCommandHandler(IProductCatalogDbContext productCatalogDbContext)
    {
        _productCatalogDbContext = productCatalogDbContext;
    }

    public async Task<bool> Handle(DeleteCatalogCommand request, CancellationToken cancellationToken)
    {
        var catalog = await _productCatalogDbContext.Categories.FindAsync(request.Id);

        if (catalog.IsNull())
        {
            throw new NotFoundException($"Catalog ({request.Id}) is not found.");
        }

        _productCatalogDbContext.Categories.Remove(catalog);

        await _productCatalogDbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
