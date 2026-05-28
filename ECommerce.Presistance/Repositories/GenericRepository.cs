using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities;
using ECommerce.Presistance.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Presistance.Repositories
{
    public class GenericRepository<TEntity, Tkey> : IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        private readonly StoreDbContext _dbContext;

        public GenericRepository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, Tkey> specifications)
        {
            IQueryable<TEntity> Query = _dbContext.Set<TEntity>();

            if(specifications is not null )
            {
                if( specifications.IncludeExpressions is not null && specifications.IncludeExpressions.Any())
                {
                    foreach(var IncludeExp in specifications.IncludeExpressions)
                    {
                        Query = Query.Include(IncludeExp);
                    }
                }
               
            }
            return await Query.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(Tkey Id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(Id);
        }


        public async Task AddAsync(TEntity entity)
        {
             await _dbContext.Set<TEntity>().AddAsync(entity);
        }




        public void Update(TEntity entity)
        {
             _dbContext.Set<TEntity>().Update(entity); 
        }

        public void Remove(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            
        }

        
    }
}
