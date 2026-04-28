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

        #region Relationship.

        public List<Product> Products { get; set; } = new List<Product>();

        #endregion
    }
}
