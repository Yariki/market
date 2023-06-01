using System.Reflection;
using Market.Shared.Infrastructure.Common;
using Market.Shared.Infrastructure.Persistance.Interceptors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ProductCatalog.Infrastructure.Persistence;

public class ProductCatalogDbContext : ApplicationDbContext<ProductCatalogDbContext>
{
    private readonly IMediator _mediator;
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public ProductCatalogDbContext(
        DbContextOptions<ProductCatalogDbContext> options,
        IMediator mediator,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor) 
        : base(options, mediator, auditableEntitySaveChangesInterceptor)
    {
    }

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
