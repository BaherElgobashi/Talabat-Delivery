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

        #region Includes
        public List<Expression<Func<TEntity, object>>> Includes { get; } = [];

       

        protected void AddIncludes(Expression<Func<TEntity, object>> IncludeExpresion)
        {
            Includes.Add(IncludeExpresion);
        }

        #endregion

    }
}
