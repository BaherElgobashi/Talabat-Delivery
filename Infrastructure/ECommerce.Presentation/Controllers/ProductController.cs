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
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public ProductController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }
        [HttpGet("Get All Products")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            var Products = await serviceManager.ProductServices.GetAllProductsAsync();

            return Ok(Products);
        }

        [HttpGet("Get All Brands")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllBrands()
        {
            var Brands = await serviceManager.ProductServices.GetAllBrandsAsync();

            return Ok(Brands);
        }

        [HttpGet("Get All Types")]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetAllTypes()
        {
            var Types = await serviceManager.ProductServices.GetAllTypesAsync();

            return Ok(Types);
        }

        [HttpGet("Get-Product/{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var Product = await serviceManager.ProductServices.GetProductByIdAsync(id);
            return Ok(Product);
        }
    }
}
