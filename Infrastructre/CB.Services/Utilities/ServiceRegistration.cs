using CB.Application.Abstractions.Services;
using CB.Application.Abstractions.Services.Authentication;
using CB.Application.Abstractions.Services.Html;
using CB.Application.Abstractions.Services.Mail;
using CB.Services.Authentication;
using CB.Services.Html;
using CB.Services.Mail;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace CB.Services.Utilities
{
    public static class ServiceRegistration
    {
        public static void AddServiceServices(this IServiceCollection services)
        {
            services.AddTransient<ILoginService,LoginService>();
            services.AddTransient<IRegistrationService,RegistrationService>();
            services.AddTransient<IEmailAccountService,EmailAccountService>();
            services.AddTransient<IEmailTemplateService, EmailTemplateService>();
            services.AddTransient<IWorkflowEmailService, WorkflowEmailService>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IHttpContextAccessor,HttpContextAccessor>();
            services.AddTransient<IWorkContext,WorkContext>();
            services.AddTransient<IHtmlNotificationService,HtmlNotificationService>();
            
        }
    }
}
