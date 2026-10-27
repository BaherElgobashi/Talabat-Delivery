using ECommerce.Abstraction.IServices;
using ECommerce.Shared.Dtos.Basket_Dtos;
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
    [Route("api/[controller]")]
    public class PaymentController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpPost("{basketId}")]
        [Authorize]
        public async Task<ActionResult<BasketDto>> CreateOrUpdatePaymentIntent(string BasketId)
        {
            var Basket = await serviceManager.paymentService.CreateUpdatePaymentIntentAsync(BasketId);

            return Ok(Basket);
        }
    }
}
