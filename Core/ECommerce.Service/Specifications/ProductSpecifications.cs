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
        // Get All Products Without Filtration.
        public ProductSpecifications(): base(null)
        {
            AddIncludes(p => p.Brand);
            AddIncludes(p => p.Type);
        }

        // Get All Products With BrandId and TypeId Filtration.
        public ProductSpecifications(int? BrandId, int? TypeId) : 
            base(p => (!BrandId.HasValue ||p.BrandId == BrandId) && (!TypeId.HasValue || p.TypeId == TypeId))
        {
            AddIncludes(p => p.Brand);
            AddIncludes(p => p.Type);
        }
        public ProductSpecifications(int id ):base(p => p.Id == id)
        {
            AddIncludes(p => p.Brand);
            AddIncludes(p => p.Type);
        }
    }
}
