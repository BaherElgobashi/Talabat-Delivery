using ECommerce.Domain.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Specifications
{
    public class OrderWithPaymentIntentSpecifications : BaseSpecifications<Order , Guid>
    {
        public OrderWithPaymentIntentSpecifications(string IntentId) : base(O => O.PaymentIntentId == IntentId)
        {
            
        }
    }
}
