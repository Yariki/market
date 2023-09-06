
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Common.Services;
using ProductCatalog.Application.Product.Models;

public class GetProductQuery : IRequest<ProductDto>
{ 
      public Guid ProductId { get; set; }
}

public class GetProoductQueryHandler : IRequestHandler<GetProductQuery, ProductDto>
{
    private readonly IProductCatalogDbContext _productCatalogDbContext;
    private readonly IMapper _mapper;

    public GetProoductQueryHandler(IProductCatalogDbContext productCatalogDbContext, 
        IMapper mapper)
    {
        _productCatalogDbContext = productCatalogDbContext;
        _mapper = mapper;
    }

    public Task<ProductDto> Handle(GetProductQuery request, CancellationToken cancellationToken) => _productCatalogDbContext.Products
            .Include(p => p.SellUnits)
            .ThenInclude(s => s.Unit)
            .Where(x => x.Id == request.ProductId)
            .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
}


public class GetProcudtQueryValidation : AbstractValidator<GetProductQuery>
{
    public GetProcudtQueryValidation()
    {
        RuleFor(c => c.ProductId)
            .NotEqual(Guid.Empty)
            .WithMessage("Product Id is required");
    }
} 