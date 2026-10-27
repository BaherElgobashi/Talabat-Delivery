using ECommerce.Abstraction.IServices;
using ECommerce.Domain.Contratcs.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Service.Services
{
    public class CacheService(ICacheRepository cacheRepository) : ICacheService
    {
        public async Task<string?> GetAsync(string CacheKey)
        {
           return await cacheRepository.GetAsync(CacheKey);
        }

        public async Task SetAsync(string CacheKey, object CacheValue, TimeSpan TimeToLive)
        {
            var Value = JsonSerializer.Serialize(CacheValue); // object Changed to JSON [object -> JSON].
                                                              
            await cacheRepository.SetAsync(CacheKey, Value, TimeToLive);
        }
    }
}
