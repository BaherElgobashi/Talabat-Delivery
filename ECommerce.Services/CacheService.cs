using ECommerce.Domain.Contracts;
using ECommerce.Services.Abstraction.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services
{
    public class CacheService : ICacheService
    {
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
