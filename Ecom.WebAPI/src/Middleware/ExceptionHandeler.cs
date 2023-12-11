using Ecom.Service.src.Shared;

namespace Ecom.WebAPI.src.Middleware
{
    public class ExceptionHandeler : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {

            try
            {
                await next(context);
            }
            catch (CustomException e)
            {
                context.Response.StatusCode = e.StatusCode;
                await context.Response.WriteAsync(e.Message);
            }
            catch (Exception e)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync(e.Message);

            }

        }
    }
}