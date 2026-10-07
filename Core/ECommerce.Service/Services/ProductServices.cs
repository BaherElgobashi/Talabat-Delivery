using ECommerce.Abstraction.IServices;
using ECommerce.Domain.Contratcs.UOW;
using ECommerce.Domain.Models.Products;
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
        private readonly IUnitOfWork unitOfWork;

        public ProductServices(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var Repo = unitOfWork.GetRepository<Product,int>();

            var Products = await Repo.GetAllAsync();

            var Products = 
        }

        public Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            throw new NotImplementedException();
        }
        public Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            throw new NotImplementedException();
        }

        

        public Task<ProductDto> GetProductById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
