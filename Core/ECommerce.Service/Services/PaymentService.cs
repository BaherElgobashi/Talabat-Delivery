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
            // 1. Configure Stripe : Install Stripe.Net.
            // 2. Get Basket By BasketId.
            // 3. Get Amount - GetProduct + DeliveryMethod Cost.
            // 4. Create Payment Intent [Create - Update].
        }
    }
}
