using System.Reflection;
using Market.Shared.Application.Interfaces;
using Market.Shared.Infrastructure.Persistance.Interceptors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Orders.Infrastructure.Persistence;

public class OrderDbContext : ApplicationDbContext<OrderDbContext>, IApplicationDbContext
{
    private readonly IMediator _mediator;
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public OrderDbContext(
        DbContextOptions<OrderDbContext> options,
        IMediator mediator,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
        : base(options, mediator, auditableEntitySaveChangesInterceptor)
    {
        _mediator = mediator;
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
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
        //TODO: Dispatch domain events
        //await _mediator.DispatchDomainEvents(this);

        return await base.SaveChangesAsync(cancellationToken);
    }
}
