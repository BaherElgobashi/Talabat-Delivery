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

        // Get All Products.
        public ProductWithTypeAndBrandSpecification() : base(null)
        {
            AddInclude(P => P.ProductType);
            AddInclude(P => P.ProductBrand);
        }


        // Get All Products.
        public ProductWithTypeAndBrandSpecification(int? brandId , int? typeId) 
            : base(P => (!brandId.HasValue || P.BrandId == brandId)
            && (!typeId.HasValue || P.TypeId == typeId))
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
