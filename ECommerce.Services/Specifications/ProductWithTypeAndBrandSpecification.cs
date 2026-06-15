using ECommerce.Domain.Entities;
using ECommerce.Domain.Entities.Products;
using ECommerce.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Specifications
{
    public class ProductWithTypeAndBrandSpecification : BaseSpecifications<Product, int>
    {

        // Get All Products.
        public ProductWithTypeAndBrandSpecification() : base(null)
        {
            AddInclude(P => P.ProductType);
            AddInclude(P => P.ProductBrand);
        }


        // Get All Products.
        public ProductWithTypeAndBrandSpecification(ProductQueryParams queryParams)
            : base(P => (!queryParams.BrandId.HasValue || P.BrandId == queryParams.BrandId.Value)
            && (!queryParams.TypeId.HasValue || P.TypeId == queryParams.TypeId.Value)
            && (string.IsNullOrEmpty(queryParams.Search) || P.Name.ToLower().Contains(queryParams.Search.ToLower()))
            )

        {
            AddInclude(P => P.ProductType);
            AddInclude(P => P.ProductBrand);
        }

        // Get Products By Id.

        public ProductWithTypeAndBrandSpecification(int id ) : base(P => P.Id == id) 
        {
                AddInclude(P => P.ProductType);
                AddInclude(P => P.ProductBrand);
        }
    }
}
