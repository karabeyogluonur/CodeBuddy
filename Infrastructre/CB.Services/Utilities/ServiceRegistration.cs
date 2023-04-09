using CB.Application.Abstractions.Services.Authentication;
using CB.Application.Abstractions.Services.Mail;
using CB.Services.Authentication;
using CB.Services.Mail;
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
            services.AddTransient<IMailTemplateService, MailTemplateService>();
            services.AddTransient<IWorkflowMailService, WorkflowMailService>();
            services.AddTransient<IEmailSender, EmailSender>();
            
        }
    }
}
