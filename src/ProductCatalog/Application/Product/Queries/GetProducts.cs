using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Common.Services;
using ProductCatalog.Application.Product.Models;

namespace Microsoft.Extensions.DependencyInjection.Product.Queries;

public class GetProductsQuery : IRequest<IEnumerable<ProductDto>>
{
    public Guid CatalogId { get; set; }
}

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductDto>>
{
    private readonly IProductCatalogDbContext _productCatalogDbContext;
    private readonly IMapper _mapper;

    public GetProductsQueryHandler(IProductCatalogDbContext productCatalogDbContext,
        IMapper mapper)
    {
        _productCatalogDbContext = productCatalogDbContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        return  await _productCatalogDbContext.Products
            .Include(p => p.SellUnits)
            .ThenInclude(s => s.Unit)
            .Where(x => x.CatalogId == request.CatalogId)
            .ProjectTo<ProductDto>(_mapper.ConfigurationProvider).ToListAsync();
    }
}