using ECommerce.Abstraction.IServices;
using ECommerce.Shared.Dtos.Basket_Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Services
{
    public class BasketServices : IBasketService
    {
        public Task<BasketDto> GetBasketAsync(string Key)
        {
            throw new NotImplementedException();
        }
        public Task<BasketDto> CreateUpdateBasketAsync(BasketDto basket)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteBasketAsync(string Key)
        {
            throw new NotImplementedException();
        }

      
    }
}
