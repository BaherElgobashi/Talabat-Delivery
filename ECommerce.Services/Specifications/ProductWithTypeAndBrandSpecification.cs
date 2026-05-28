using ECommerce.Domain.Entities;
using ECommerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Specifications
{
    public class ProductWithTypeAndBrandSpecification : BaseSpecifications<Product ,int>
    {
        public ProductWithTypeAndBrandSpecification() : base()
        {
            AddInclude(P => P.ProductType);
            AddInclude(P => P.ProductBrand);
        }
    }
}
