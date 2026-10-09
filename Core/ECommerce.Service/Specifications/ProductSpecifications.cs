using ECommerce.Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Specifications
{
    public class ProductSpecifications : BaseSpecifications<Product, int>
    {
        public ProductSpecifications(): base(null)
        {
            AddIncludes(p => p.Brand);
            AddIncludes(p => p.Type);
        }
    }
}
