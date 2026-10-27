using ECommerce.Abstraction.IServices;
using ECommerce.Presentation.Attributes;
using ECommerce.Shared.Common;
using ECommerce.Shared.Dtos.ProductDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Presentation.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public ProductController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }
        [HttpGet("Products")]
        [Cache]
        public async Task<ActionResult<PaginationResult<ProductDto>>> GetAllProducts([FromQuery]ProductQueryParams productQueryParams)
        {
            var Products = await serviceManager.ProductServices.GetAllProductsAsync(productQueryParams);

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
