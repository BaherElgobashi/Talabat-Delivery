using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.Dtos.OrderDtos
{
    public class OrderItemDto
    {
        public string ProductName { get; set; } = null!;
        public string ProductPictureUrl { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
