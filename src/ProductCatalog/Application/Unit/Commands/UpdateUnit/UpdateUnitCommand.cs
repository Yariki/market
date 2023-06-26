using Market.Shared.Application.Exceptions;
using Market.Shared.Infrastructure.Common.Extensions;
using MediatR;
using ProductCatalog.Application.Common.Services;

namespace Microsoft.Extensions.DependencyInjection.Unit.Commands.UpdateUnit;

public class UpdateUnitCommand : IRequest<Guid>
{
    public Guid UnitId { get; set; }
    public string Abbriviation { get; set; }
    public string Description { get; set; }
}


public class UpdateUnitCommandHandler : IRequestHandler<UpdateUnitCommand, Guid>
{
    private readonly IProductCatalogDbContext _productCatalogDbContext;
    
    public UpdateUnitCommandHandler(IProductCatalogDbContext productCatalogDbContext)
    {
        _productCatalogDbContext = productCatalogDbContext;
    }
    
    public async Task<Guid> Handle(UpdateUnitCommand request, CancellationToken cancellationToken)
    {
        var unit = await _productCatalogDbContext.Units.FindAsync(request.UnitId, cancellationToken);

        if (unit.IsNull())
        {
            throw new NotFoundException($"Unit with {request.UnitId} was not found.");
        }

        unit.Abbriviation = request.Abbriviation;
        unit.Description = request.Description;
        
        await _productCatalogDbContext.SaveChangesAsync(cancellationToken);

        return unit.Id;
    }
}
