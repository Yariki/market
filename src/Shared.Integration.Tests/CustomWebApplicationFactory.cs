using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;

namespace Shared.Integration.Tests;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> 
    where TProgram : class
{
    private readonly PostgreSqlContainer _container;

    public CustomWebApplicationFactory()
    {
        _container = new PostgreSqlBuilder()
                        .WithDatabase("market")
                        .Build();
        _container.StartAsync().GetAwaiter().GetResult();
    }

    protected override void Dispose(bool disposing)
    {
        _container.DisposeAsync().GetAwaiter().GetResult();
        base.Dispose(disposing);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((context, config) =>
        {
            var integrationConfig = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
            config.AddConfiguration(integrationConfig);
        });

        builder.ConfigureServices((builder,services) =>
        {
            this.ConfigurationServices(builder, services);
        });
    }

    protected virtual void ConfigurationServices(WebHostBuilderContext builder,
     IServiceCollection services)
    {
    }

}
