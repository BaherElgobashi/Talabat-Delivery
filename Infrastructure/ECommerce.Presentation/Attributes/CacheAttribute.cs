using ECommerce.Abstraction.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Presentation.Attributes
{
    public class CacheAttribute(int DurationInSeconds = 90) : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Create Cache Key.
            string CacheKey = CreateCacheKey(context.HttpContext.Request);

            // Search For Value With Cache Key.
            ICacheService cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();

            var CacheValue = await cacheService.GetAsync(CacheKey);

            // Return Value is not null.
            if(CacheValue is not null)
            {
                context.Result = new ContentResult()
                {
                    Content = CacheValue,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK,
                };
                return;
            }
            // Return Value is null.

            // Invoke Next.
            var ExecutedContext = await next.Invoke();

            // Set Value With Cache Key.
            if(ExecutedContext.Result is ObjectResult result)
            {
                await cacheService.SetAsync(CacheKey , result.Value , TimeSpan.FromSeconds(DurationInSeconds));
            }






            //return base.OnActionExecutionAsync(context, next);
        }






        private string CreateCacheKey(HttpRequest request)
        {

            StringBuilder key = new StringBuilder();

            key.Append(request.Path + "?");
            foreach (var item in request.Query.OrderBy(Q => Q.Key))
            {
                key.Append($"{item.Key}={item.Value}&");
            }

            return key.ToString();
        }
    }

    
}
