using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Contratcs.Specifications
{
    //store the different parts of the query
    public interface ISpecification <TEntity , TKey> where TEntity : BaseEntity<TKey>
    {
        // Where Condition.
        Expression<Func<TEntity,bool>> Criteria { get; }

        // Order.
        Expression<Func<TEntity, object>> OrderBy { get; }

        // OrderBy.
        Expression<Func<TEntity, object>> OrderByDesc { get; }

        // Includes.
        List<Expression<Func<TEntity , object>>>Includes  { get; }

        // Pagination.
        int Take { get; }
        int Skip { get; }
        bool IsPaginated { get; set; }
    }
}
