using CB.Application.Models.User.Authentication;
using CB.Application.Utilities;
using CB.Application.Utilities.Defaults;
using CB.Application.Validations.User.Authentication;
using CB.Data.Utilities;
using CB.Services.Utilities;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;

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
                x.RegisterValidatorsFromAssemblyContaining<LoginViewModelValidator>();
            });

            
        }
        public static void AddAuthenticationConfigures(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequiredLength = 8;      
            });
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Authentication/Login";
                options.LogoutPath = "/Authenticaton/Logout";
                options.ExpireTimeSpan = TimeSpan.FromDays(1);
                options.Cookie.Name = CookieDefaults.Prefix + CookieDefaults.Authentication;
            });
        }
    }
}
