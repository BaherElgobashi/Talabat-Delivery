using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities;
using ECommerce.Presistance.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Presistance.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _dbContext;

        public UnitOfWork(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IGenericRepository<TEntity, TKey> GetGenericRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            
        }
    }
}
