using Market.Identity.Api.Data;
using Market.Shared.Application.Mappings;

namespace Market.Identity.Api.Application.Users.Models;

public class UserViewModel : IMapFrom<AuthUser>
{
    public string ProfileImageName { get; set; } = string.Empty;
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Username { get; set; } = string.Empty;
    
    public string Email { get; set; } = string.Empty;
    
    public string PhoneNumber { get; set; } = string.Empty;
}