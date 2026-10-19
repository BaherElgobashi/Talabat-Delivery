using ECommerce.Shared.Common;
using ECommerce.Shared.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Abstraction.IServices
{
    public interface IProductServices
    {
        Task<PaginationResult<ProductDto>> GetAllProductsAsync(ProductQueryParams productQueryParams);
        Task<IEnumerable<TypeDto>> GetAllTypesAsync();
        Task<IEnumerable<BrandDto>> GetAllBrandsAsync();
        Task<ProductDto> GetProductByIdAsync(int id);
    }
}
