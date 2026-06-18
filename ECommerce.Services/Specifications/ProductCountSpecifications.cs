using ECommerce.Domain.Entities.Products;
using ECommerce.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Specifications
{
    public class ProductCountSpecifications : BaseSpecifications<Product, int>
    {
        public ProductCountSpecifications(ProductQueryParams queryParams) 
            : base(ProductSpecificationsHelper.GetProductCriteria(queryParams))
        {
            
        }
    }
}
