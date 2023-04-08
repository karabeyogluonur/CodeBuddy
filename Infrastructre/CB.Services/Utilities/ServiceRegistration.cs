using CB.Application.Abstractions.Services.Authentication;
using CB.Services.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace CB.Services.Utilities
{
    public static class ServiceRegistration
    {
        public static void AddServiceServices(this IServiceCollection services)
        {
            services.AddTransient<ILoginService,LoginService>();
            services.AddTransient<IRegistrationService,RegistrationService>();
        }
    }
}
