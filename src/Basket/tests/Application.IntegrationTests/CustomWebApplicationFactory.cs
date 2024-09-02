using Basket.Application.Common.Interfaces;
using Market.Shared.Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Basket.Application.IntegrationTests;

using static Testing;

internal class CustomWebApplicationFactory : WebApplicationFactory<Program>
{


    public CustomWebApplicationFactory()
    {
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
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
            services
                .Remove<ICurrentUserService>()
                .AddTransient(provider => Mock.Of<ICurrentUserService>(s =>
                    s.UserId == GetCurrentUserId()));

            this.ConfigureServices(builder, services);
        });
    }

    protected virtual void ConfigureServices(WebHostBuilderContext builder, IServiceCollection services)
    {
    }

}