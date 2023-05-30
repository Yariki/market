using ProductCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ProductCatalog.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }

    DbSet<TEntity> Set<TEntity>() where TEntity: class;

    EntityEntry<TEntity> Entry<TEntity>(TEntity entity)  where TEntity: class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
