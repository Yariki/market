using Market.Shared.Integration.Tests;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductCatalog.Infrastructure.Persistence;

namespace ProductCatalog.Application.IntegrationTests
{
    internal class ProductCatalogApplicationFactory : CustomWebApplicationFactory<Program>
    {
        protected override void ConfigureServices(WebHostBuilderContext builder, IServiceCollection services)
        {
            services
           .RemoveDbContext<ProductCatalogDbContext>();

            services.AddDbContext<ProductCatalogDbContext>(
                opt => opt.UseSqlServer(GetConnectionString()
                ));

            base.ConfigureServices(builder, services);
        }
    }
}
