using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.Basket_Module;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Presistance.Repositories
{
    public class BasktetRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasktetRepository(IConnectionMultiplexer connection)
        {
            _database = connection.GetDatabase();
        }

        public async Task<CustomerBasket?> CreateOrUpdateAsync(CustomerBasket basket, TimeSpan timeToLive = default)
        {
            var JsonBasket = JsonSerializer.Serialize(basket);
            var IsCreatedOrUpdated = await _database.StringSetAsync(basket.Id , JsonBasket ,
                (timeToLive == default) ? TimeSpan.FromDays(7) : timeToLive);
            if (IsCreatedOrUpdated)
            {
                var Basket = await _database.StringGetAsync(basket.Id);
                return JsonSerializer.Deserialize<CustomerBasket>(Basket!);
            }
            else
            {
                return null;
            }
        }

        public Task<bool> DeleteBasketAsync(string basketId)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerBasket?> GetBasketAsync(string basketId)
        {
            throw new NotImplementedException();
        }
    }
}
