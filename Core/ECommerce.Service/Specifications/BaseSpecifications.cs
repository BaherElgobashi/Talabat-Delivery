using ECommerce.Domain.Contratcs.Specifications;
using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Specifications
{
    public abstract class BaseSpecifications<TEntity, TKey> : ISpecification<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {

        #region Where.
        protected BaseSpecifications(Expression<Func<TEntity , bool>> _Criteria)
        {
            //only a private set for Criteria to be set only in the constructor.

              Criteria = _Criteria;
        }


        public Expression<Func<TEntity, bool>> Criteria { get; private set; } //store the where condition.

        #endregion

        #region OrderBy and OrderByDesc.
        public Expression<Func<TEntity, object>> OrderBy { get; private set; }

        public Expression<Func<TEntity, object>> OrderByDesc { get; private set; }

        protected void AddOrderBy(Expression<Func<TEntity, object>> _OrderBy)
        {
            OrderBy = _OrderBy;
        }

        protected void AddOrderByDesc(Expression<Func<TEntity, object>> _OrderByDesc)
        {
            OrderByDesc = _OrderByDesc;
        }

        #endregion

        #region Includes.
        public List<Expression<Func<TEntity, object>>> Includes { get; } = [];

        protected void AddIncludes(Expression<Func<TEntity, object>> IncludeExpresion)
        {
            Includes.Add(IncludeExpresion);
        }

        #endregion

        #region Pagination.
        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPaginated { get;  set; }

        public void ApplyPagination(int PageIndex , int PageSize )
        {
            IsPaginated = true;
            Take = PageSize;
            Skip = (PageIndex - 1) * PageSize;
        }


        #endregion

    }
}
