using ECommerce.Domain.Contratcs.Specifications;
using ECommerce.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Persistence
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity , TKey> (IQueryable<TEntity> BaseQuery , ISpecification<TEntity , TKey> specification)
            where TEntity : BaseEntity<TKey>
        {
            var query = BaseQuery;

            if(specification is not null)
            {
                // Where Condition.
                if(specification.Criteria is not null)
                {
                    query = query.Where(specification.Criteria);
                }

                // Includes.

                if(specification.Includes is not null && specification.Includes.Any())
                {
                    foreach (var include in specification.Includes)
                    {
                        query.Include(include);
                    }

                    //both has the same peroformance using foreach or aggregate method but the for each is more readable.

                    //query = specification.Includes.Aggregate(query , (current , include) => current.Include(include));
                }


            }
            return query;
        }
    }
}
