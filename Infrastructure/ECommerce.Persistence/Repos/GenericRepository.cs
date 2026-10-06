using ECommerce.Domain.Contratcs.Repos;
using ECommerce.Domain.Models;
using ECommerce.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Persistence.Repos
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext context;

        public GenericRepository(StoreDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
           return await context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetById(TKey id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }
        public void Add(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
        }
        public void Update(TEntity entity)
        {
            context.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
        }

        

        
    }
}
