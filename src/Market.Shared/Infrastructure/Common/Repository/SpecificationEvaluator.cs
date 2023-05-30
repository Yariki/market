﻿using Market.Shared.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Market.Shared.Infrastructure.Common.Repository;

public class SpecificationEvaluator<T> where T : class
{
    public static IQueryable<T> GetQuery(IQueryable<T> input, ISpecification<T> spec)
    {
        var query = input;
        query = query.Where(spec.Criteria);

        if (spec.OrderBy != null)
        {
            query = query.OrderBy(spec.OrderBy);
        }

        if (spec.OrderByDescending != null)
        {
            query = query.OrderByDescending(spec.OrderByDescending);
        }

        query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

        return query;
    }
}