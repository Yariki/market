using Market.Shared.Application.Interfaces;

namespace Market.Shared.Infrastructure.Services;

public class TenantProvider : ITenantProvider
{
    private readonly ICurrentUserService _currentUserService;
    
    public TenantProvider(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }
    
    public string? GetTenantId()
    {
        if (_currentUserService.IsAdmin())
        {
            return null;
        }

        return _currentUserService.UserId;
    }
}