using Microsoft.AspNetCore.Identity;

namespace Market.Identity.Api.Data;

public class AuthUser : IdentityUser
{
    public string ProfileImageName { get; set; } = string.Empty;
}

