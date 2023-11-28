using Market.Identity.Api.Data;
using Market.Shared.Application.Interfaces;
using Market.Shared.Integration.Tests;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Market.Identity.Api.IntegrationTests;

public class IdentityApplicationFactory :  CustomWebApplicationFactory<Program>
{
    protected override void ConfigureServices(WebHostBuilderContext builder,
        IServiceCollection services)
    {
        services.Remove<IHttpContextAccessor>()
            .AddTransient(provider => Mock.Of<IHttpContextAccessor>(
                accessor => accessor.HttpContext == new DefaultHttpContext()
            ));
        services.Remove<ICurrentUserService>()
            .AddScoped(provider => Mock.Of<ICurrentUserService>(
                               service => service.UserId == Guid.NewGuid().ToString()
            ));
        
        services.RemoveDbContext<ApplicationDbContext>();
        
        services.AddDbContext<ApplicationDbContext>(
            opt => opt.UseSqlServer(GetConnectionString()
            ));
        
        base.ConfigureServices(builder, services);
    }
}