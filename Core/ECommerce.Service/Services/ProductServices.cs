using AutoMapper;
using ECommerce.Abstraction.IServices;
using ECommerce.Domain.Contratcs.UOW;
using ECommerce.Domain.Models.Products;
using ECommerce.Service.Specifications;
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
        private readonly IMapper mapper;

        public ProductServices(IUnitOfWork unitOfWork , IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        // old one with no includes
        //public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        //{
        //    var Repo = unitOfWork.GetRepository<Product,int>();

            

        //    var Products = await Repo.GetAllAsync();

        //    var ProductDto = mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(Products);

        //    return ProductDto;
        //}

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var Repo = unitOfWork.GetRepository<Product,int>();

            var Spec = new ProductSpecifications();

            var Products = await Repo.GetAllWithSpecificationAsync(Spec);

            var ProductDto = mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(Products);

            return ProductDto;
        }
        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var Repo = unitOfWork.GetRepository<ProductType , int>();

            var Types = await Repo.GetAllAsync();

            var TypesDto = mapper.Map<IEnumerable<ProductType> , IEnumerable<TypeDto>>(Types);

            return TypesDto;
        }
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var Repo = unitOfWork.GetRepository<ProductBrand , int>();

            var Brands = await Repo.GetAllAsync();

            var BrandsDto = mapper.Map<IEnumerable<ProductBrand> , IEnumerable<BrandDto>>(Brands);

            return BrandsDto;
        }


        // GetById With No Includes.
        //public async Task<ProductDto> GetProductByIdAsync(int id)
        //{
        //    var Repo = unitOfWork.GetRepository<Product, int>();

        //    var Product = await Repo.GetByIdAsync(id);

        //    var ProductDto = mapper.Map<Product, ProductDto>(Product);

        //    return ProductDto;
        //}

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var Repo = unitOfWork.GetRepository<Product, int>();
            var spec = new ProductSpecifications(id);

            var Product = await Repo.GetByIdWithSpecificationAsync(spec);

            var ProductDto = mapper.Map<Product, ProductDto>(Product);

            return ProductDto;
        }
    }
}
