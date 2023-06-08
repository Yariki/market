using Microsoft.EntityFrameworkCore;
using ProductCatalog.Domain.Catalog;
using ProductCatalog.Domain.Product;

namespace Microsoft.Extensions.DependencyInjection.Common.Services;

public interface IProductCatalogDbContext
{
    DbSet<Product> Products { get; }
    DbSet<Unit> Units { get; }
    DbSet<Catalog> Categories { get; }
    DbSet<SellUnit> SellUnits { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}