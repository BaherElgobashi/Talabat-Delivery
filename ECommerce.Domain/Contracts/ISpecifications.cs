using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Contracts
{
    public interface ISpecifications<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public ICollection<Expression<Func<TEntity, object>>> IncludeExpressions { get; }

        public Expression<Func<TEntity, bool>> Criteria { get; }

        public Expression<Func<TEntity , object>> OrderBy { get; }
        public Expression<Func<TEntity , object>> OrderByDescending { get; }

    }
}
