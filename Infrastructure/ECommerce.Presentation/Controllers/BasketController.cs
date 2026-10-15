using ECommerce.Abstraction.IServices;
using ECommerce.Shared.Dtos.Basket_Dtos;
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
    public class BasketController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public BasketController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasketAsync(string Key)
        {
            var Basket = await serviceManager.BasketServices.GetBasketAsync(Key);

            return Ok(Basket);
        }

        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateUpdateBasketAsync(BasketDto basket)
        {
            var InternalBasket = await serviceManager.BasketServices.CreateUpdateBasketAsync(basket);
            return Ok(InternalBasket);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteBasketAsync(string Key)
        {
            var Result = await serviceManager.BasketServices.DeleteBasketAsync(Key);
            return Ok(Result);
        }
    }
}
