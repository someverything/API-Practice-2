using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.Middlewears
{
    public class LocalizationMiddlewear : IMiddleware
    {
        private readonly ILogger<LocalizationMiddlewear> _logger;

        public LocalizationMiddlewear(ILogger<LocalizationMiddlewear> logger)
        {
            _logger = logger;
        }

        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var lang = context.Request.Headers["accept-language"].FirstOrDefault() ?? "az";
            CultureInfo culture;

            switch (lang)
            {
                case "az":
                case "en-US":
                case "ru-RU":
                    culture = new CultureInfo(lang);
                    break;
                default:
                    culture = new CultureInfo("az");
                    break;
            }

            _logger.LogInformation($"Set culture name {culture.Name}");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            return next(context);
        }
    }
}
