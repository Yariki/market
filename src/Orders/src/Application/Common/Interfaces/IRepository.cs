using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Application.Common.Interfaces;
public interface IRepository
{
}


public interface IWriteRepository<TId, TEntity> : IRepository where TEntity: class
{
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<List<TEntity>> GetAllAsync(string include = null);
    Task<TEntity> GetByIdAsync(TId id, string include = null);
    Task<TEntity> GetByAsync(Expression<Func<TEntity, bool>> selector, 
        string include = null);
    Task<List<TEntity>> GetAllByAsync(Expression<Func<TEntity, bool>> selector, 
        string include = null);
    Task<TEntity> DeleteAsync(TId id);

    Task<IEnumerable<TEntity>> GetWithSpecification(ISpecification<TEntity> specification);
}


public interface IReadRepository<TId, TEntity> : IRepository where TEntity: class
{
    Task<TEntity> GetBySpecificationAsync(ISpecification<TEntity> spec, CancellationToken cancellation);

    Task<IEnumerable<TEntity>> GetListBySpecificationAsync(ISpecification<TEntity> spec, CancellationToken cancellation);
}
