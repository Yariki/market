using Market.Identity.Api.Data;
using Market.Shared.Application.Mappings;
using Microsoft.AspNetCore.Identity;

namespace Market.Identity.Api.Application.Roles.Models;

public class RoleViewModel : IMapFrom<AuthRole>
{
    public string Id { get; set; }
    
    public string Name { get; set; }
}