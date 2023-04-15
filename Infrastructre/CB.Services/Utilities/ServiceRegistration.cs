using CB.Application.Abstractions.Services;
using CB.Application.Abstractions.Services.Authentication;
using CB.Application.Abstractions.Services.Features;
using CB.Application.Abstractions.Services.Html;
using CB.Application.Abstractions.Services.Mail;
using CB.Application.Abstractions.Services.Media;
using CB.Application.Abstractions.Services.Membership;
using CB.Application.Abstractions.Services.Security;
using CB.Services.Authentication;
using CB.Services.Features;
using CB.Services.Html;
using CB.Services.Mail;
using CB.Services.Media;
using CB.Services.Membership;
using CB.Services.Security;
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
            services.AddTransient<IEncryptionService,EncryptionService>();
            services.AddTransient<IUserService,UserService>();
            services.AddTransient<ITalentService, TalentService>(); 
            services.AddTransient<IFileService,FileService>();
            
        }
    }
}
