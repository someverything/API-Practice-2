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
            catch (ArgumentNullException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest, "Invalid Argument");
            }
            catch (UnauthorizedAccessException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.Unauthorized, "Unauthorized Access");
            }
            catch (InvalidOperationException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest, "Invalid Operation");
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError, "Server error");
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex, HttpStatusCode statusCode, string title)
        {
            _logger.LogError(ex, ex.Message);

            context.Response.StatusCode = (int)statusCode;
            ProblemDetails details = new()
            {
                Status = (int)statusCode,
                Title = title,
                Type = title,
                Detail = ex.Message
            };

            var json = JsonSerializer.Serialize(details);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(json);
        }
    }
}
