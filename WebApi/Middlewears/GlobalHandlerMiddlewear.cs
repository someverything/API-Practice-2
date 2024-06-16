using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebApi.Middlewears
{
    public class GlobalHandlerMiddlewear : IMiddleware
    {
        private readonly ILogger<GlobalHandlerMiddlewear> _logger;

        public GlobalHandlerMiddlewear(ILogger<GlobalHandlerMiddlewear> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {

            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                ProblemDetails details = new()
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Title = "Server error",
                    Type = "Server error",
                    Detail = ex.Message
                };

                var json = JsonSerializer.Serialize(details);
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json);

            }

        }
    }
}
