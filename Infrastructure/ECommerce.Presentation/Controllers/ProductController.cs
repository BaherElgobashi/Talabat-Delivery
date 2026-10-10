using ECommerce.Abstraction.IServices;
using ECommerce.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public ProductController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }
        [HttpGet("Products")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts(int? BrandId , int? TypeId)
        {
            var Products = await serviceManager.ProductServices.GetAllProductsAsync(BrandId , TypeId);

            return Ok(Products);
        }

        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllBrands()
        {
            var Brands = await serviceManager.ProductServices.GetAllBrandsAsync();

            return Ok(Brands);
        }

        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetAllTypes()
        {
            var Types = await serviceManager.ProductServices.GetAllTypesAsync();

            return Ok(Types);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var Product = await serviceManager.ProductServices.GetProductByIdAsync(id);
            return Ok(Product);
        }


    }
}
