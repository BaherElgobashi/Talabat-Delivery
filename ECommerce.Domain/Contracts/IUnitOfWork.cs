using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Contracts
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity , TKey> GetGenericRepository<TEntity , TKey>() where TEntity : BaseEntity<TKey>;

        Task<int> SaveChangesAsync();
    }
}
