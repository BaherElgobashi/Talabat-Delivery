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
        private readonly Dictionary<Type, object> _repositories = [];

        public UnitOfWork(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IGenericRepository<TEntity, TKey> GetGenericRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var EntityType = typeof(TEntity);
            if(_repositories.TryGetValue(EntityType, out object? repository))
            {
                return (IGenericRepository<TEntity, TKey>) repository;
            }

            var newRepo = new GenericRepository<TEntity, TKey>(_dbContext);

            _repositories[EntityType] = newRepo;

            return newRepo;
        }

        public Task<int> SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}
