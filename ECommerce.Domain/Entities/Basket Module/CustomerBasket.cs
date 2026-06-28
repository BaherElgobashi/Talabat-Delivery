using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Entities.Basket_Module
{
    public class CustomerBasket
    {
        public string Id { get; set; } = default!; // GUID : Created from Frontend.

        public ICollection<BasketItem> Items { get; set; } = [];
    }
}
