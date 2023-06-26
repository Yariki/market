
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Common.Services;
using ProductCatalog.Application.Product.Models;

public class GetProoductQuery : IRequest<ProductDto>
{ 
      public Guid ProductId { get; set; }
}

public class GetProoductQueryHandler : IRequestHandler<GetProoductQuery, ProductDto>
{
    private readonly IProductCatalogDbContext _productCatalogDbContext;
    private readonly IMapper _mapper;

    public GetProoductQueryHandler(IProductCatalogDbContext productCatalogDbContext, 
        IMapper mapper)
    {
        _productCatalogDbContext = productCatalogDbContext;
        _mapper = mapper;
    }

    public Task<ProductDto> Handle(GetProoductQuery request, CancellationToken cancellationToken) => _productCatalogDbContext.Products
            .Include(p => p.SellUnits)
            .ThenInclude(s => s.Unit)
            .Where(x => x.Id == request.ProductId)
            .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
}