using ECommerce.Domain.Models.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Contratcs.Repos
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketAsync(string key);

        Task<CustomerBasket?> CreateUpdateBasketAsync(CustomerBasket basket , TimeSpan? TimeToLive = null);
        Task<bool> DeleteBasketAsync(string key);

    }
}
