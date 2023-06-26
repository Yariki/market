using Microsoft.EntityFrameworkCore;
using ProductCatalog.Domain.Catalog;
using ProductCatalog.Domain.Product;
using UnitEntity = ProductCatalog.Domain.Product.Unit;

namespace ProductCatalog.Application.Common.Services;

public interface IProductCatalogDbContext
{
    DbSet<ProductCatalog.Domain.Product.Product> Products { get; }
    DbSet<UnitEntity> Units { get; }
    DbSet<ProductCatalog.Domain.Catalog.Catalog> Categories { get; }
    DbSet<SellUnit> SellUnits { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}