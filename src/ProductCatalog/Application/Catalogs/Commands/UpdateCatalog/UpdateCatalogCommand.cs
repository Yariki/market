using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Market.Shared.Application.Exceptions;
using Market.Shared.Infrastructure.Common.Extensions;
using MediatR;
using ProductCatalog.Application.Common.Services;

namespace ProductCatalog.Application.Catalogs.Commands.UpdateCatalog;
public  class UpdateCatalogCommand : IRequest<Guid>
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

}


public class UpdateCatalogCommandHandler : IRequestHandler<UpdateCatalogCommand, Guid>
{
    private readonly IProductCatalogDbContext _productCatalogDbContext;

    public UpdateCatalogCommandHandler(IProductCatalogDbContext productCatalogDbContext)
    {
        _productCatalogDbContext = productCatalogDbContext;
    }

    public async Task<Guid> Handle(UpdateCatalogCommand request, CancellationToken cancellationToken)
    {
        var catalog = await _productCatalogDbContext.Categories.FindAsync(request.Id);

        if (catalog.IsNull())
        {
            throw new NotFoundException($"Catalog ({request.Id.ToString()}) is not found");
        }

        catalog.Name = request.Name;
        catalog.Description = request.Description;

        await _productCatalogDbContext.SaveChangesAsync(cancellationToken);

        return catalog.Id;

    }
}
