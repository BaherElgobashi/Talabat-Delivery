using ECommerce.Domain.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Presistance.Repositories
{
    public class CacheRepository : ICacheRepository
    {
        private readonly IDatabase _database;
        
        public CacheRepository(IConnectionMultiplexer connection)
        {
            _database = connection.GetDatabase();
            
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
