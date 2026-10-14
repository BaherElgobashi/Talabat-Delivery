using ECommerce.Domain.Contratcs.Repos;
using ECommerce.Domain.Models.Baskets;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Persistence.Repos
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IConnectionMultiplexer connection;

        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer connection)
        {
            
            _database = connection.GetDatabase();
        }
        public async Task<CustomerBasket?> GetBasketAsync(string key)
        {
            var Basket = await _database.StringGetAsync(key);

            if (Basket.IsNullOrEmpty)
                return null;
            else
                return JsonSerializer.Deserialize<CustomerBasket>(Basket);
        }
        public async Task<CustomerBasket?> CreateUpdateBasketAsync(CustomerBasket basket, TimeSpan? TimeToLive = null)
        {
            var JsonBasket = JsonSerializer.Serialize(basket);
            var IsCreatedorUpdated = await _database.StringSetAsync(basket.Id , JsonBasket , TimeToLive ?? TimeSpan.FromHours(5));

            if (IsCreatedorUpdated)
                return await GetBasketAsync(basket.Id);
            else
                return null;

        }

        public async Task<bool> DeleteBasketAsync(string key)
        {
            return await _database.KeyDeleteAsync(key);
        }

        
    }
}
