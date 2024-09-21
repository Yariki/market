namespace Market.Shared.Application.Interfaces;

public interface ITenantProvider
{
    string? GetTenantId();   
}