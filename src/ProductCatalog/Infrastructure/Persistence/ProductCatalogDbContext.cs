using System.Reflection;
using System.Security.Cryptography.X509Certificates;
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

    public ProductCatalogDbContext(
        DbContextOptions<ProductCatalogDbContext> options,
        IMediator mediator,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor, 
        IHttpContextAccessor accessor) 
        : base(options, mediator, auditableEntitySaveChangesInterceptor)
    {
#if !DEBUG
        // var conn = Database.GetDbConnection() as SqlConnection;
        // if(conn != null)
        // {
        //     conn.AccessToken = accessor?.HttpContext?.Request.Headers["X-MS-TOKEN-AAD-ACCESS-TOKEN"];
        // }
#endif
    }

    public DbSet<Product> Products => Set<Product>();
    
    public DbSet<Unit> Units => Set<Unit>();
    
    public DbSet<Catalog> Categories => Set<Catalog>();
    
    public DbSet<SellUnit> SellUnits => Set<SellUnit>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

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
