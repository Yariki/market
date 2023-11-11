using Market.Shared.Application.Interfaces;
using Market.Shared.Infrastructure.Persistance.Interceptors;
using Market.Shared.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using ProductCatalog.Application.Common.Services;
using ProductCatalog.Infrastructure.Persistence;
using DateTimeService = ProductCatalog.Infrastructure.Services.DateTimeService;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ProductCatalogDbContext>(options =>
                options.UseInMemoryDatabase("ProductCatalogDb"));
        }
        else
        {
            services.AddDbContext<ProductCatalogDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), 
                options => options.EnableRetryOnFailure()));
        }

        services.AddScoped<IProductCatalogDbContext>(provider => provider.GetRequiredService<ProductCatalogDbContext>());
        services.AddScoped<ApplicationDbContextInitialiser>();

        return services;
    }
}