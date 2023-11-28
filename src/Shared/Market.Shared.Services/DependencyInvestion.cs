using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Market.Shared.Application.Interfaces;
using Market.Shared.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Market.Shared.Services;
public static class DependencyInvestion
{
    public static IServiceCollection AddSharedServices(this IServiceCollection services)
    {
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        
        return services;
    }
}
