using MediatR;
using ProductCatalog.Application.Common.Services;
using UnitEntity = ProductCatalog.Domain.Product.Unit;

namespace ProductCatalog.Application.Unit.Commands.AddUnit;

public class AddUnitCommand : IRequest<Guid>
{
    public string Abbriviation { get; set; }
    public string Description { get; set; }
}

public class AddUnitCommandHandler : IRequestHandler<AddUnitCommand, Guid>
{
    private readonly IProductCatalogDbContext _productCatalogDbContext;

    public AddUnitCommandHandler(IProductCatalogDbContext productCatalogDbContext)
    {
        _productCatalogDbContext = productCatalogDbContext;
    }

    public async Task<Guid> Handle(AddUnitCommand request, CancellationToken cancellationToken)
    {
        var unit = UnitEntity.CreateUnit(request.Abbriviation, request.Description);

        await _productCatalogDbContext.Units.AddAsync(unit, cancellationToken); 
        await  _productCatalogDbContext.SaveChangesAsync(cancellationToken);

        return unit.Id;
    }
}



