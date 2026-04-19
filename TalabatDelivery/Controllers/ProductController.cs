using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TalabatDelivery.Models;

namespace TalabatDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ProductController : ControllerBase
    {
        [HttpGet("{id}")]
        public ActionResult<Product> GetById(int id)
        {
            return new Product { Id = id , Name = "test" };
        }

        [HttpGet]

        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            return new List<Product>()
            {
                new Product(){Id = 1, Name = "test1"},
                new Product(){Id = 1, Name = "test2"},
                new Product(){Id = 3, Name = "test3"},
                new Product(){Id = 4, Name = "test4"}
            };
        }
    }
}
