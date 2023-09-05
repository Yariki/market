using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Catalogs.Models;
using ProductCatalog.Application.Common.Services;

namespace ProductCatalog.Application.Catalogs.Queries.GetCatalogs;

public class GetCatalogsQuery : IRequest<IEnumerable<CatalogDto>>
{
}


public class GetCatalogsQueryHandler : IRequestHandler<GetCatalogsQuery, IEnumerable<CatalogDto>>
{
    private readonly IProductCatalogDbContext _productProductCatalogDbContext;
    private readonly IMapper _mapper;

    public GetCatalogsQueryHandler(IProductCatalogDbContext productCatalogDbContext, IMapper mapper)
    {
        _productProductCatalogDbContext = productCatalogDbContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CatalogDto>> Handle(GetCatalogsQuery request, CancellationToken cancellationToken)
    {
        var catalogs = await _productProductCatalogDbContext
            .Categories
            .ProjectTo<CatalogDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return catalogs;
    }
}