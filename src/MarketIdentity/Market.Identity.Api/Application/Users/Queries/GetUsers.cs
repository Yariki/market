using AutoMapper;
using AutoMapper.QueryableExtensions;
using Market.Identity.Api.Application.Users.Models;
using Market.Identity.Api.Data;
using Market.Shared.Application.Mappings;
using Market.Shared.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Market.Identity.Api.Application.Users.Queries;

public class GetUsersQuery : IRequest<PaginatedList<UserViewModel>>
{
    public int PageNumber { get; set; }
    
    public int PageSize { get; set; }
}

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PaginatedList<UserViewModel>>
{
    private readonly UserManager<AuthUser> _userManager;
    private readonly IMapper _mapper;

    public GetUsersQueryHandler(UserManager<AuthUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<PaginatedList<UserViewModel>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userManager
            .Users
            .ProjectTo<UserViewModel>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);

        return users;
    }
}
