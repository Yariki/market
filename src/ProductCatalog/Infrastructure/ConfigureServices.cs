using Market.Shared.Application.Interfaces;
using Market.Shared.Infrastructure.Persistance.Interceptors;
using ProductCatalog.Infrastructure.Persistence;
using ProductCatalog.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly(typeof(ProductCatalogDbContext).Assembly.FullName)));
        }

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ProductCatalogDbContext>());

        services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddTransient<IDateTime, DateTimeService>();

        services.AddAuthentication()
            .AddIdentityServerJwt();

        services.AddAuthorization();

        return services;
    }
}
