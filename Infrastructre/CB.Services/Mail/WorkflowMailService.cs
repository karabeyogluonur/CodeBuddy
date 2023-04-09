using CB.Application.Abstractions.Services.Mail;
using CB.Application.Utilities.Defaults;
using CB.Domain.Entities.Mail;
using CB.Domain.Entities.Membership;

namespace CB.Services.Mail
{
    public class WorkflowMailService : IWorkflowMailService
    {
        IMailTemplateService _MailTemplateService;
        IEmailSender _emailSender;

        public WorkflowMailService(IMailTemplateService MailTemplateService, IEmailSender emailSender)
        {
            _MailTemplateService = MailTemplateService;
            _emailSender = emailSender;
        }

        protected virtual async Task<MailTemplate> GetActiveMailTemplateAsync(string MailTemplateName)
        {
            var MailTemplate = await _MailTemplateService.GetMailTemplatesByNameAsync(MailTemplateName);
            if (MailTemplate == null || !MailTemplate.Active)
                return null;
            else
                return MailTemplate;

        }
        public async Task SendUserWelcomeMessageAsync(AppUser appUser)
        {
            if (appUser == null)
                throw new ArgumentNullException(nameof(appUser));

            var MailTemplates = await GetActiveMailTemplateAsync(MailTemplateDefaults.UserWelcomeMessage);
            if (MailTemplates == null)
                return;

            MailTemplates.Body = MailTemplates.Body.Replace("%User.FirstName%", appUser.FirstName).Replace("%User.LastName%", appUser.LastName);
            await _emailSender.SendEmailAsync(MailTemplates.EmailAccount, MailTemplates.Subject, MailTemplates.Body, "CodeBuddy", "info@codebuddy.com", appUser.Email, appUser.FirstName);
        }

        public async Task SendUserEmailConfirmationMessageAsync(AppUser appUser, string emailConfirmationToken)
        {
            if (appUser == null)
                throw new ArgumentNullException(nameof(appUser));

            var MailTemplates = await GetActiveMailTemplateAsync(MailTemplateDefaults.UserEmailConfirmationMessage);
            if (MailTemplates == null)
                return;

            MailTemplates.Body = MailTemplates.Body.Replace("%Email.ConfirmationToken%", emailConfirmationToken)
                                                         .Replace("%User.FirstName%", appUser.FirstName)
                                                         .Replace("%User.LastName%", appUser.LastName);

            await _emailSender.SendEmailAsync(MailTemplates.EmailAccount, MailTemplates.Subject, MailTemplates.Body, "Codebuddy", "info@codebuddy.com", appUser.Email, appUser.FirstName);

        }
	}
}
