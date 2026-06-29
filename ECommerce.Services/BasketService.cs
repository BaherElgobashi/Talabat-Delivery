using ECommerce.Services.Abstraction.Services;
using ECommerce.Shared.DTOS.BasketDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services
{
    public class BasketService : IBasketService
    {
        public Task<BasketDTO> CreateOrUpdateBasketAsync(BasketDTO basket)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteBasketAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<BasketDTO> GetBasketAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
