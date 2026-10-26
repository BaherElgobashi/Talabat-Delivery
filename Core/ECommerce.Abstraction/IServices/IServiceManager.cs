using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Abstraction.IServices
{
    public interface IServiceManager
    {
        public IProductServices ProductServices { get; }
        public IBasketService BasketServices { get; }
        public IAuthenticationService authenticationService { get; }

        public IOrderService orderServices { get; }
    }
}
