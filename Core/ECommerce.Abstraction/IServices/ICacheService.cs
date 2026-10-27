using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Abstraction.IServices
{
    public interface ICacheService
    {
        // Get.
        Task<string?> GetAsync(string CacheKey);

        // Set.
        Task SetAsync(string CacheKey , object CacheValue , TimeSpan TimeToLive);
    }
}
