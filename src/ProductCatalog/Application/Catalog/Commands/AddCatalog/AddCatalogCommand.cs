using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProductCatalog.Application.Common.Services;
using CatalogEntity = ProductCatalog.Domain.Catalog.Catalog;

namespace ProductCatalog.Application.Catalog.Commands.AddCatalog;
public class AddCatalogCommand : IRequest<Guid>
{
    public string Name { get; set; }

    public string Description { get; set; }
}

public class AddCatalogCommandHandler : IRequestHandler<AddCatalogCommand, Guid>
{
    private readonly IProductCatalogDbContext _productCatalogDbContext;

    public AddCatalogCommandHandler(IProductCatalogDbContext productCatalogDbContext)
    {
        _productCatalogDbContext = productCatalogDbContext;
    }

    public async Task<Guid> Handle(AddCatalogCommand request, CancellationToken cancellationToken)
    {
        var catalog = new CatalogEntity()
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description
        };

        await _productCatalogDbContext.Categories.AddAsync(catalog);

        await _productCatalogDbContext.SaveChangesAsync(cancellationToken);

        return catalog.Id;
    }
}


