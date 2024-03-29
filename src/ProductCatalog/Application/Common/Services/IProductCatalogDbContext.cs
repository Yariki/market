﻿using Microsoft.EntityFrameworkCore;
using ProductCatalog.Domain.Catalogs;
using ProductCatalog.Domain.Product;
using UnitObject = ProductCatalog.Domain.Product.Unit;

namespace ProductCatalog.Application.Common.Services;

public interface IProductCatalogDbContext
{
    DbSet<ProductCatalog.Domain.Product.Product> Products { get; }
    DbSet<UnitObject> Units { get; }
    DbSet<ProductCatalog.Domain.Catalogs.Catalog> Categories { get; }
    DbSet<SellUnit> SellUnits { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}