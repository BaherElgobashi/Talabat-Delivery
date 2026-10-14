using ECommerce.Shared.Dtos.Basket_Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Abstraction.IServices
{
    public interface IBasketService
    {
        Task<BasketDto> GetBasketAsync(string Key);
        Task<BasketDto> CreateUpdateBasketAsync(BasketDto basket);
        Task<bool> DeleteBasketAsync(string Key);
    }
}
