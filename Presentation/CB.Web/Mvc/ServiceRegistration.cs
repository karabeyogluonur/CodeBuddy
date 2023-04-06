using CB.Application.Utilities;
using CB.Data.Utilities;
using CB.Services.Utilities;
using FluentValidation.AspNetCore;

namespace CB.Web.Mvc
{
    public static class ServiceRegistration
    {
        public static void AddBaseServices(this IServiceCollection services)
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
        }
        public static void AddLayerServices(this IServiceCollection services)
        {
            services.AddDataServices();
            services.AddApplicationServices();
            services.AddServiceServices();
        }
        public static void AddFrameworkServices(this IServiceCollection services)
        {
            // Automapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Fluent Validation
            services.AddFluentValidation(x =>
            {
                x.DisableDataAnnotationsValidation = true;
                x.RegisterValidatorsFromAssemblyContaining<Program>();
            });

            
        }
        public static void AddAuthenticationConfigures(this IServiceCollection services)
        {

		}
    }
}
