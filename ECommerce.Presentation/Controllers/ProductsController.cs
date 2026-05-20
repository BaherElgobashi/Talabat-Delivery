using ECommerce.Services.Abstraction.Services;
using ECommerce.Shared.DTOS.ProductDTOS;
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
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // Get All Products.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProductsAsync()
        {
            var Products = await _productService.GetAllProductsAsync();

            return Ok(Products);
        }




        // Get All Brands.
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandDTO>>> GetAllBrandsAsync()
        {
            var Brands = await _productService.GetAllBrandsAsync();

            return Ok(Brands);
        }

        // Get All Types.
        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<TypeDTO>>> GetAllTypesAsync()
        {
            var Types = await _productService.GetAllTypesAsync();

            return Ok(Types);
        }

    }
}
