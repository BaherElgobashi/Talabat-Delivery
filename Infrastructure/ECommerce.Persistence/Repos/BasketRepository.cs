using ECommerce.Domain.Contratcs.Repos;
using ECommerce.Domain.Models.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Persistence.Repos
{
    public class BasketRepository : IBasketRepository
    {
        public Task<CustomerBasket?> GetBasketAsync(string key)
        {
            throw new NotImplementedException();
        }
        public Task<CustomerBasket?> CreateUpdateBasketAsync(CustomerBasket basket, TimeSpan? TimeToLive = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteBasketAsync(string key)
        {
            throw new NotImplementedException();
        }

        
    }
}
