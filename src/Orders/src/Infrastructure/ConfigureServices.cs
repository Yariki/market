using Market.Shared.Infrastructure.Persistance.Interceptors;
using Orders.Infrastructure.Persistence;
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
            services.AddDbContext<OrderDbContext>(options =>
                options.UseInMemoryDatabase("OrdersDb"));
        }
        else
        {
            services.AddDbContext<OrderDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly(typeof(OrderDbContext).Assembly.FullName)));
        }

        services.AddScoped<OrderDbContext>(provider => provider.GetRequiredService<OrderDbContext>());

        services.AddScoped<ApplicationDbContextInitialiser>();

        //TODO: Add other services
        
        //services.AddIdentityServer()
        //    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

        //services.AddTransient<IDateTime, DateTimeService>();

        services.AddAuthentication()
            .AddIdentityServerJwt();

        services.AddAuthorization(options =>
            options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator")));

        return services;
    }
}
