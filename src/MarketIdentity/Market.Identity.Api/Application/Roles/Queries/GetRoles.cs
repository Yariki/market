using AutoMapper;
using AutoMapper.QueryableExtensions;
using Market.Identity.Api.Application.Roles.Models;
using Market.Identity.Api.Data;
using Market.Shared.Application.Mappings;
using Market.Shared.Application.Models;
using MediatR;

namespace Market.Identity.Api.Application.Roles.Queries;

public class GetRolesQuery : IRequest<PaginatedList<RoleViewModel>>
{
    public int PageNumber { get; set; }
    
    public int PageSize { get; set; }
}

public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, PaginatedList<RoleViewModel>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    
    public GetRolesQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<RoleViewModel>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        var result = await _context
            .Roles
            .ProjectTo<RoleViewModel>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);

        return result;
    }
}