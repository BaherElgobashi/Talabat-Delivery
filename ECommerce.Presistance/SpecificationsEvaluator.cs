using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ECommerce.Presistance
{
    public static class SpecificationsEvaluator
    {
        public static IQueryable<TEntity> CreateTEntity<TEntity , TKey> (IQueryable<TEntity> EntryPoint ,
            ISpecifications<TEntity, TKey> specifications) where TEntity : BaseEntity<TKey>
        {
            var Query = EntryPoint;
            if (specifications is not null)
            {
                // Include Condition.
                if (specifications.IncludeExpressions is not null && specifications.IncludeExpressions.Any())
                {
                    //foreach (var IncludeExp in specifications.IncludeExpressions)
                    //{
                    //    Query = Query.Include(IncludeExp);
                    //}

                    Query = specifications.IncludeExpressions.Aggregate(Query , (CurrentQuery , IncludeExp) => CurrentQuery.Include(IncludeExp));
                }

                // Criteria Condition.
                if (specifications.Criteria is not null)
                {
                    Query = Query.Where(specifications.Criteria);
                }

                if(specifications.OrderBy is not null)
                {
                    Query = Query.OrderBy(specifications.OrderBy);
                }

                if (specifications.OrderByDescending is not null)
                {
                    Query = Query.OrderByDescending(specifications.OrderByDescending);
                }

                if (specifications.IsPaginated)
                {
                    Query = Query.Skip(specifications.Skip).Take(specifications.Take);
                }

            }
            return Query;
        }
    }
}
