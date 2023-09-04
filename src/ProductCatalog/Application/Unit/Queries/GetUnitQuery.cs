using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Common.Services;

namespace ProductCatalog.Application;

public class GetUnitQuery : IRequest<IEnumerable<UnitDto>>
{
}

public class GetUnitQueryHandler : IRequestHandler<GetUnitQuery, IEnumerable<UnitDto>>
{
    private readonly IProductCatalogDbContext _context;
    private readonly IMapper _mapper;

    public GetUnitQueryHandler(IProductCatalogDbContext context, 
                                IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UnitDto>> Handle(GetUnitQuery request, CancellationToken cancellationToken)
    {
        var units = await _context.Units.AsNoTracking()
            .ProjectTo<UnitDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return units;
    }
}
