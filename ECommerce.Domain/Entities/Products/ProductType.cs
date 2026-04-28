using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Entities.Products
{
    public class ProductType
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
    }
}
