using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;

namespace Market.Shared.Integration.Tests;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{

    private readonly MsSqlContainer _testcontainer;

    public CustomWebApplicationFactory()
    {
        _testcontainer = new MsSqlBuilder().Build();

        _testcontainer.StartAsync().GetAwaiter().GetResult();
    }

    protected override void Dispose(bool disposing)
    {
        _testcontainer.DisposeAsync().GetAwaiter().GetResult();
        base.Dispose(disposing);
    }

    protected string GetConnectionString()
    {
        return _testcontainer.GetConnectionString();
    }

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
            this.ConfigureServices(builder, services);
        });

    }

    protected virtual void ConfigureServices(WebHostBuilderContext builder, IServiceCollection services)
    {

    }
}


