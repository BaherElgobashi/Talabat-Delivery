using ECommerce.Shared.Dtos.Basket_Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Abstraction.IServices
{
    public interface IPaymentService
    {
        Task<BasketDto> CreateUpdatePaymentIntentAsync(string BasketId);
    }
}
