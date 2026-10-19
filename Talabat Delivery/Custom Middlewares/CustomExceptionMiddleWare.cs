using Azure.Core;
using ECommerce.Domain.Exceptions;

using ECommerce.Shared.ErrorModels;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Talabat_Delivery.Custom_Exception_Middleware
{
    public class CustomExceptionMiddleWare
    {
        private readonly RequestDelegate next;
        private readonly ILogger<CustomExceptionMiddleWare> logger;

        public CustomExceptionMiddleWare(RequestDelegate Next , ILogger<CustomExceptionMiddleWare>logger)
        {
            next = Next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next.Invoke(context);

                if(context.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    #region Response Body.

                    // Response Object.
                    var Response = new ErrorToReturn()
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = $"Endpoint {context.Request.Path} is not Found."
                    };

                    // Return Object as JSON.
                    await context.Response.WriteAsJsonAsync(Response);

                    #endregion
                }

            }
            catch(Exception ex)
            {
                logger.LogError(ex , ex.Message);

                #region Response Header.

                // Set Status Code For Response.
                context.Response.StatusCode = ex switch 
                {
                    UnAuthorizedException => StatusCodes.Status401Unauthorized,
                    BadRequestException => StatusCodes.Status400BadRequest,
                    NotFoundException  => StatusCodes.Status404NotFound,

                    _ => StatusCodes.Status500InternalServerError
                };
                    
                      
                    

                // Set Message For Response.
                context.Response.ContentType = "application/json";


                #endregion

                #region Response Body.

                // Response Object.
                var Response = new ErrorToReturn()
                {
                    StatusCode = context.Response.StatusCode,
                    Message = ex.Message
                };

                // Return Object as JSON.
                await context.Response.WriteAsJsonAsync(Response);

                #endregion
            }
        }

        private int GetBadRequestErrors(BadRequestException exception , ErrorToReturn response)
        {
            response.Errors = exception.Errors;
            return StatusCodes.Status400BadRequest;

        }

    }
}
