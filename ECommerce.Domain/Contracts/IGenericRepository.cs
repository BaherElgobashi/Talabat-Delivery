using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Contracts
{
    public interface IGenericRepository<TEntity , TKey> where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>>GetAllAsync();

        Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity , TKey> specifications);

        Task <TEntity?> GetByIdAsync(TKey Id);
        Task <TEntity?> GetByIdAsync(ISpecifications<TEntity , TKey> specifications);

        Task AddAsync(TEntity entity);

        void Update(TEntity entity);
        void Remove(TEntity entity);

        Task<int> CountAsync(ISpecifications<TEntity, TKey> specifications);
    }
}
