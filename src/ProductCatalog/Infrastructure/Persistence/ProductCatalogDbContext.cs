using System.Reflection;
<<<<<<< HEAD
using System.Security.Cryptography.X509Certificates;
=======
using Market.Shared.Application.Interfaces;
>>>>>>> 754124c (added tenant filter for products)
using Market.Shared.Infrastructure.Common;
using Market.Shared.Infrastructure.Persistance.Interceptors;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Common.Services;
using ProductCatalog.Domain.Catalogs;
using ProductCatalog.Domain.Product;
using Unit = ProductCatalog.Domain.Product.Unit;

namespace ProductCatalog.Infrastructure.Persistence;

public class ProductCatalogDbContext : ApplicationDbContext<ProductCatalogDbContext>, IProductCatalogDbContext
{
    private readonly IMediator _mediator;
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;
    private readonly ITenantProvider _tenantProvider;

    public ProductCatalogDbContext(
        DbContextOptions<ProductCatalogDbContext> options,
        IMediator mediator,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor, ITenantProvider tenantProvider) 
        : base(options, mediator, auditableEntitySaveChangesInterceptor)
    {
        _tenantProvider = tenantProvider;
    }

    public DbSet<Product> Products => Set<Product>();
    
    public DbSet<Unit> Units => Set<Unit>();
    
    public DbSet<Catalog> Categories => Set<Catalog>();
    
    public DbSet<SellUnit> SellUnits => Set<SellUnit>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        var tenantId = _tenantProvider.GetTenantId();

        if (!string.IsNullOrEmpty(tenantId))
        {
            builder.Entity<Product>().HasQueryFilter(p => EF.Property<string?>(p, "CreatedBy") == tenantId);    
        }
        
        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEvents(this);

        return await base.SaveChangesAsync(cancellationToken);
    }
}
