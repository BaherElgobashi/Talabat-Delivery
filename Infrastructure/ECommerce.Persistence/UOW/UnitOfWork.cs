using ECommerce.Domain.Contratcs.Repos;
using ECommerce.Domain.Contratcs.UOW;
using ECommerce.Domain.Models;
using ECommerce.Persistence.Contexts;
using ECommerce.Persistence.Repos;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Persistence.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext context;

        public UnitOfWork(StoreDbContext context)
        {
            this.context = context;
        }
        private readonly Dictionary<string, object> _Repos = [];

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var TypeName = typeof(TEntity).Name;

            if (_Repos.ContainsKey(TypeName))
            {
                return (IGenericRepository<TEntity, TKey>)_Repos[TypeName];
            }

            else
            {
                var Repo = new GenericRepository<TEntity, TKey>(context);
                _Repos.Add(TypeName, Repo);
                return Repo;
            }

            

        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
