using AutoMapper;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.Products;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;

            _mapper = mapper;
        }
        public async Task<IEnumerable<BrandDTO>> GetAllBrandsAsync()
        {

            var Brands = await _unitOfWork.GetGenericRepository<ProductBrand , int>().GetAllAsync();
            return _mapper.Map<IEnumerable<BrandDTO>>(Brands);
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
