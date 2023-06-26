using Market.Shared.Application.Exceptions;
using Market.Shared.Infrastructure.Common.Extensions;
using MediatR;
using ProductCatalog.Application.Common.Services;

namespace Microsoft.Extensions.DependencyInjection.Unit.Commands.DeleteUnit;

public class DeleteUnitCommand : IRequest
{
    public Guid UnitId { get; set; }
}

public class DeleteUnitCommandHandler : IRequestHandler<DeleteUnitCommand>
{
    private readonly IProductCatalogDbContext _productCatalogDbContext;
    
    public DeleteUnitCommandHandler(IProductCatalogDbContext productCatalogDbContext)
    {
        _productCatalogDbContext = productCatalogDbContext;
    }
    
    public async Task Handle(DeleteUnitCommand request, CancellationToken cancellationToken)
    {
        var unit = _productCatalogDbContext.Units.Find(request.UnitId);

        if (unit.IsNull())
        {
            throw new NotFoundException($"Unit with {request.UnitId} was not found.");
        }

        _productCatalogDbContext.Units.Remove(unit);
        await _productCatalogDbContext.SaveChangesAsync(cancellationToken);

    }
}