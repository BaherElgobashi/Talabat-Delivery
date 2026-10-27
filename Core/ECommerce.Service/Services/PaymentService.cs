using ECommerce.Abstraction.IServices;
using ECommerce.Shared.Dtos.Basket_Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Services
{
    public class PaymentService : IPaymentService
    {
        public Task<BasketDto> CreateUpdatePaymentIntentAsync(string BasketId)
        {
            throw new NotImplementedException();
        }
    }
}
