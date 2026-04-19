using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TalabatDelivery.Models;

namespace TalabatDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public ActionResult<Product> GetById(int id)
        {
            return new Product { Id = id , Name = "test" };
        }
    }
}
