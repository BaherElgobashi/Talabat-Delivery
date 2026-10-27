using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Presentation.Attributes
{
    public class CacheAttribute : ActionFilterAttribute
    {







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
