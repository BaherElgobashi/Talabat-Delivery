using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Specifications
{
    public abstract class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {

        #region Includes.

        // Include Implementation.
        public ICollection<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];
        protected void AddInclude(Expression<Func<TEntity, object>> IncludeExp)
        {
            IncludeExpressions.Add(IncludeExp);
        } 
        #endregion

        #region Criteria.

        // Criteria Implementation.
        public Expression<Func<TEntity, bool>> Criteria { get; }


        protected BaseSpecifications(Expression<Func<TEntity, bool>> criteriaExpression)
        {
            Criteria = criteriaExpression;
        }
        #endregion



        #region Sorting.

        public Expression<Func<TEntity, object>> OrderBy { get; private set; }

        public Expression<Func<TEntity, object>> OrderByDescending  { get; private set; }

        protected void AddOrderBy(Expression<Func<TEntity, object>> OrderByExpression)
        {
            OrderBy = OrderByExpression;
        }

        protected void AddOrderByDescending(Expression<Func<TEntity, object>> OrderByDescendingExpression)
        {
            OrderByDescending = OrderByDescendingExpression;
        }

        #endregion
    }
}
