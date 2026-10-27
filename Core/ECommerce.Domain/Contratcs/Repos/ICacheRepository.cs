using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Contratcs.Repos
{
    public interface ICacheRepository
    {
        // Get Cahce.
        Task<string?> GetAsync(string CacheKey);

        // Set Cache.
        Task SetAsync(string CacheKey , string CacheValue , TimeSpan TimeToLive);
    }
}
