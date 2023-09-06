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
        IQueryable<ProductCatalog.Domain.Product.Product> query = _productCatalogDbContext.Products
            .Include(p => p.SellUnits)
            .ThenInclude(s => s.Unit);
            
        if (request.CatalogId != Guid.Empty)
        {
            query = query
                .Where(x => x.CatalogId == request.CatalogId);
        }            
        
        var products = await query
            .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return products;
    }
}