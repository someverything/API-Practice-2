
using System.Globalization;

namespace WebApi.Middlewears
{
    //todo Delegate
    //todo Middleware
    //todo Auth
    public class LocalizationMiddlewear : IMiddleware
    {
        public Task InvokeAsync(HttpContext context, RequestDelegate next)
            {
            var lang = context.Request.Headers["accept-language"].FirstOrDefault() ?? "az";
            if (lang == "az" || lang == "en-US" || lang == "ru-RU")
            {
                var culture = new CultureInfo(lang);
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
                return next(context);
            }
            else
            {
                var culture = new CultureInfo("az");
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
                return next(context);
            }
        }


    }
}
