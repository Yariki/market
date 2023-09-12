using Market.Shared.Integration.Tests;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using ProductCatalog.Infrastructure.Persistence;

namespace ProductCatalog.Application.IntegrationTests
{
    internal class ProductCatalogApplicationFactory : CustomWebApplicationFactory<Program>
    {
        protected override void ConfigureServices(WebHostBuilderContext builder, IServiceCollection services)
        {
            services
                .Remove<IHttpContextAccessor>()
                .AddTransient(provider => Mock.Of<IHttpContextAccessor>(
                    accessor => accessor.HttpContext == new DefaultHttpContext()
                ));

            services
                .RemoveDbContext<ProductCatalogDbContext>();

            services.AddDbContext<ProductCatalogDbContext>(
                opt => opt.UseSqlServer(GetConnectionString()
                ));

            base.ConfigureServices(builder, services);
        }
    }
}
