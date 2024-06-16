using Core.Utilities.Security.Abstract;
using Core.Utilities.Security.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace Core.DependenyResolver
{
    public static class ServisRegistration
    {
        public static void AddCoreService(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenManager>();
        }
    }
}
