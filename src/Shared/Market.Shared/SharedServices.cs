using Market.Shared.Application.Interfaces;
using Market.Shared.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Market.Shared;

public static class SharedServices
{
    public static IServiceCollection AddSharedDependencies(this IServiceCollection services)
    {
        services.AddScoped<IDateTime, DateTimeService>();
        services.AddScoped<ITenantProvider, TenantProvider>();

        return services;
    }
}