
using System.Text.Json;
using System;

namespace AppHouse.BootsStrap.Middlewares
{
    public class GlobalErrorMiddleware : IMiddleware
    {
        public static JsonSerializerOptions JsonOptions { get; } = new JsonSerializerOptions() 
        {
            DefaultBufferSize = 1024
        };

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
			try
			{
                await next(context);
			}
			catch (Exception ex)
			{
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var response = new 
                {
#if DEBUG
                    inner = ex.InnerException?.Message,
#endif
                    error = ex.Message

                };

                var jsonResponse = JsonSerializer.Serialize(response, JsonOptions);

                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(jsonResponse);
            }
        }
    }
}
