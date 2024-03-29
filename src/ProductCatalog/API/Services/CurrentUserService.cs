﻿using System.Security.Claims;
using Market.Shared.Application.Interfaces;

namespace ProductCatalog.WebUI.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    
    public bool IsAdmin()
    {
        return _httpContextAccessor.HttpContext?.User?.IsInRole("Admin") ?? false;
    }
}
