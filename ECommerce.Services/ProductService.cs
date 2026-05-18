using ECommerce.Services.Abstraction.Services;
using ECommerce.Shared.DTOS.ProductDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services
{
    public class ProductService : IProductService
    {
        public Task<IEnumerable<BrandDTO>> GetAllBrandsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TypeDTO>> GetAllTypesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProductDTO> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
