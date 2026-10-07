using ECommerce.Abstraction.IServices;
using ECommerce.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Services
{
    public class ProductServices : IProductServices
    {
        public Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProductDto> GetProductById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
