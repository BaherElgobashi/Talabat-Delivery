using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Models.Orders
{
    public class Order : BaseEntity<int>
    {
        public string UserEmail { get; set; } = null!;
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderAddress Address { get; set; } = null!;
        public DeliveryMethod DeliveryMethod { get; set; } = null!;
        [ForeignKey("DeliveryMethod")]
        public int DeliveryMethodId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public ICollection<OrderItem> Items { get; set; } = [];
        public decimal SubTotal { get; set; }
        public decimal GetTotal()
        {
            return SubTotal + DeliveryMethod.Price;
        }
    }
}
