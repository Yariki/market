using Microsoft.AspNetCore.Identity;

namespace Market.Identity.Api.Data;

public class AuthRole : IdentityRole
{
    public AuthRole(string name) 
        : base(name)
    {
    }
}