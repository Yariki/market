using Market.Shared.Application.Interfaces;
using Orders.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Testcontainers.MsSql;


namespace Orders.Application.IntegrationTests;

using static Testing;

internal class CustomWebApplicationFactory : WebApplicationFactory<Program>
{

    private readonly MsSqlContainer _msSqlContainer;

    public CustomWebApplicationFactory()
    {
        _msSqlContainer =  new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2019-latest")
            .Build();
    }

    protected override void Dispose(bool disposing)
    {
        _msSqlContainer.DisposeAsync().GetAwaiter().GetResult();
        base.Dispose(disposing);
    }

    public string? ConnectionString => _msSqlContainer?.GetConnectionString();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(configurationBuilder =>
        {
            var integrationConfig = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            configurationBuilder.AddConfiguration(integrationConfig);
        });

        builder.ConfigureServices((builder, services) =>
        {
            services
                .Remove<ICurrentUserService>()
                .AddTransient(provider => Mock.Of<ICurrentUserService>(s =>
                    s.UserId == GetCurrentUserId()));

            services
                .Remove<DbContextOptions<OrderDbContext>>()
                .AddDbContext<OrderDbContext>((sp, options) =>
                    options.UseSqlServer(ConnectionString,
                        builder => builder.MigrationsAssembly(typeof(OrderDbContext).Assembly.FullName)));

            this.ConfigureServices(builder, services);
        });
    }

    protected virtual void ConfigureServices(WebHostBuilderContext builder, IServiceCollection services)
    {
    }

}
