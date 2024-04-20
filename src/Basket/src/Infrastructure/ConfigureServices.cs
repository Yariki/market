using Basket.Application.Common.Interfaces;
using Basket.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Common.Models;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {

        configuration.Bind(nameof(RedisSettings), new RedisSettings());
        
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetValue<string>("RedisSettings:ConnectionString");
            options.InstanceName = "Basket_";
        });
        
        services.AddTransient<IDateTime, DateTimeService>();
        services.AddScoped<IBasketRepository, BasketRepository>();
        
        return services;
    }
}
