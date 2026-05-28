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
                if (specifications.IncludeExpressions is not null && specifications.IncludeExpressions.Any())
                {
                    foreach (var IncludeExp in specifications.IncludeExpressions)
                    {
                        Query = Query.Include(IncludeExp);
                    }
                }

            }
            return Query;
        }
    }
}
