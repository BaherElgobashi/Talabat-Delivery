using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.Basket_Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Presistance.Repositories
{
    public class BasktetRepository : IBasketRepository
    {


        public Task<CustomerBasket?> CreateOrUpdateAsync(CustomerBasket basket, TimeSpan timeToLive = default)
        {
            throw new NotImplementedException();
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
