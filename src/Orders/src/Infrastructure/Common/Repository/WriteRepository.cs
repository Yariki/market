using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Orders.Application.Common.Interfaces;
using Orders.Domain.Common;
using Orders.Infrastructure.Common.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Orders.Infrastructure.Common.Repository;
public class WriteRepository<TId, TEntity> 
    : IWriteRepository<TId, TEntity> where TEntity : BaseEntity<TId>
{
    private readonly IApplicationDbContext _dbContext;

    public WriteRepository(IApplicationDbContext context)
    {
        _dbContext = context;
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _dbContext.Set<TEntity>().AddAsync(entity).ConfigureAwait(false);
        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        return entity;
    }

    public Task<List<TEntity>> GetAllAsync(string include = null)
    {
        var query = !string.IsNullOrEmpty(include)
            ? _dbContext.Set<TEntity>().Include(include)
            : _dbContext.Set<TEntity>();
        return query.ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync(TId id, string include = null)
    {
        var entity = !string.IsNullOrEmpty(include)
            ? await _dbContext.Set<TEntity>().Where(CompareIds(id)).Include(include).FirstOrDefaultAsync()
            : await _dbContext.Set<TEntity>().Where(CompareIds(id)).FirstOrDefaultAsync();
        return entity;
    }

    public async Task<TEntity> GetByAsync(Expression<Func<TEntity, bool>> selector, string include = null)
    {
        var query = _dbContext.Set<TEntity>().Where(selector);
        if (!string.IsNullOrEmpty(include))
        {
            query = query.Include(include);
        }

        return await query.FirstOrDefaultAsync();
    }

    public Task<List<TEntity>> GetAllByAsync(Expression<Func<TEntity, bool>> selector, string include = null)
    {
        var query = _dbContext.Set<TEntity>().Where(selector);
        if (!string.IsNullOrEmpty(include))
        {
            query = query.Include(include);
        }

        return query.ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetWithSpecification(ISpecification<TEntity> specification)
    {
        var query = _dbContext.Set<TEntity>().AsQueryable();

        query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));

        query = specification.Criteria.IsNotNull()
            ? query.Where(specification.Criteria)
            : query;

        query = specification.OrderBy.IsNotNull()
            ? query.OrderBy(specification.OrderBy)
            : specification.OrderByDescending.IsNotNull()
                ? query.OrderByDescending(specification.OrderByDescending)
                : query;

        return await query.ToListAsync().ConfigureAwait(false);
    }

    public async Task<TEntity> DeleteAsync(TId id)
    {
        var entity = await _dbContext.Set<TEntity>().FindAsync(id);
        if (entity == null)
        {
            throw new KeyNotFoundException(nameof(id));
        }
        _dbContext.Set<TEntity>().Remove(entity);
        return entity;
    }

    protected virtual Expression<Func<TEntity, bool>> CompareIds(TId id) 
    {
        return e => Comparer<TId>.Default.Compare(e.Id, id) == 0;
    }


}
