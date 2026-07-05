using ECommerce.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Presistance.Repositories
{
    public class CacheRepository : ICacheRepository
    {
        
        public CacheRepository()
        {
            
        }
        public Task<string?> GetAsync(string CacheKey)
        {
            throw new NotImplementedException();
        }

        public Task SetAsync(string CacheKey, string CacheValue, TimeSpan TimeToLive)
        {
            throw new NotImplementedException();
        }
    }
}
