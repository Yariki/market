using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Market.Shared.Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TEntity> Set<TEntity>() where TEntity: class;

    EntityEntry<TEntity> Entry<TEntity>(TEntity entity)  where TEntity: class;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}