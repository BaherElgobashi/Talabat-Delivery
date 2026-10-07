using ECommerce.Domain.Contratcs.Repos;
using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Contratcs.UOW
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity :BaseEntity<TKey>;

        Task<int> SaveChangesAsync();
    }
}
