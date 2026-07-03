using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Presentation.Attribures
{
    public class RedisCacheAttribute : ActionFilterAttribute
    {
        
        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            // Get Cache Service From DI Container.
            // Check If Cached Data Exists.
            // If Exists , Return Cached Data and Skip Executing of EndPoint.
            // If Not Exists , Execute the EndPoint and Store the Result in Cache.
            return base.OnActionExecutionAsync(context, next);
        }
    }
}
