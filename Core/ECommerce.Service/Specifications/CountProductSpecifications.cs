using ECommerce.Domain.Models.Products;
using ECommerce.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Specifications
{
    public class CountProductSpecifications : BaseSpecifications<Product, int>
    {
        public CountProductSpecifications(ProductQueryParams productQueryParams) :
           base(p => (!productQueryParams.BrandId.HasValue || p.BrandId == productQueryParams.BrandId) && (!productQueryParams.TypeId.HasValue || p.TypeId == productQueryParams.TypeId)
            && (string.IsNullOrEmpty(productQueryParams.SearchValue) || p.Name.ToLower().Contains(productQueryParams.SearchValue.ToLower())))
        {
        }
    }
}
