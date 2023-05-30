using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ProductCatalog.Application.Common.Interfaces;

namespace ProductCatalog.Infrastructure.Common.Repository;
public class BaseSpecification<T> : ISpecification<T>
{
    protected BaseSpecification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }

    public Expression<Func<T, bool>> Criteria { get; }
    public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
    public Expression<Func<T, object>> OrderBy { get; private set; }
    public Expression<Func<T, object>> OrderByDescending { get; private set; }

    protected void AddIncludes(Expression<Func<T, object>> include)
    {
        Includes.Add(include);
    }

    protected void SetOrderByExpression(Expression<Func<T, object>> orderBy)
    {
        OrderBy = orderBy;
    }

    protected void SetOrderByDescendingExpression(Expression<Func<T, object>> orderByDescending)
    {
        OrderByDescending = orderByDescending;
    }
}
