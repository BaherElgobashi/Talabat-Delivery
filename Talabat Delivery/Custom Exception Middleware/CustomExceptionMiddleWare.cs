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
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }
    }
}
