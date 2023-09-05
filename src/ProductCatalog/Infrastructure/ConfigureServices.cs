using Market.Shared.Application.Interfaces;
using Market.Shared.Infrastructure.Persistance.Interceptors;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProductCatalog.Application.Common.Services;
using ProductCatalog.Infrastructure.Persistence;
using ProductCatalog.Infrastructure.Services;

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
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }

        services.AddScoped<IProductCatalogDbContext>(provider => provider.GetRequiredService<ProductCatalogDbContext>());

        services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddTransient<IDateTime, DateTimeService>();

        services.AddAuthentication()
            .AddIdentityServerJwt();

        services.AddAuthorization();

        return services;
    }
}
