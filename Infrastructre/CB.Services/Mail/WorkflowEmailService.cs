using CB.Application.Abstractions.Services.Mail;
using CB.Application.Utilities.Defaults;
using CB.Domain.Entities.Mail;
using CB.Domain.Entities.Membership;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;
using System.Text;
using System.Web;

namespace CB.Services.Mail
{
    public class WorkflowEmailService : IWorkflowEmailService
    {
        IEmailTemplateService _emailTemplateService;
        IEmailSender _emailSender;

        public WorkflowEmailService(IEmailTemplateService emailTemplateService, IEmailSender emailSender)
        {
            _emailTemplateService = emailTemplateService;
            _emailSender = emailSender;
        }

        protected virtual async Task<EmailTemplate> GetActiveEmailTemplateAsync(string emailTemplateName)
        {
            var emailTemplate = await _emailTemplateService.GetEmailTemplatesByNameAsync(emailTemplateName);
            if (emailTemplate == null || !emailTemplate.Active)
                return null;
            else
                return emailTemplate;

        }
        public async Task SendUserWelcomeEmailAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var mailTemplates = await GetActiveEmailTemplateAsync(MailTemplateDefaults.UserWelcomeMessage);
            if (mailTemplates == null)
                return;

            mailTemplates.Body = mailTemplates.Body.Replace("%User.FirstName%", user.FirstName).Replace("%User.LastName%", user.LastName);
            await _emailSender.SendEmailAsync(mailTemplates.EmailAccount, mailTemplates.Subject, mailTemplates.Body, "CodeBuddy", "info@codebuddy.com", user.Email, user.FirstName);
        }

        public async Task SendUserConfirmationEmailAsync(User user, string emailConfirmationToken)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var emailTemplates = await GetActiveEmailTemplateAsync(MailTemplateDefaults.UserEmailConfirmationMessage);
            if (emailTemplates == null)
                return;

            emailTemplates.Body = emailTemplates.Body.Replace("%Email.ConfirmationToken%", emailConfirmationToken)
                                                         .Replace("%User.FirstName%", user.FirstName)
                                                         .Replace("%User.LastName%", user.LastName);

            await _emailSender.SendEmailAsync(emailTemplates.EmailAccount, emailTemplates.Subject, emailTemplates.Body, "Codebuddy", "info@codebuddy.com", user.Email, user.FirstName);

        }

        public async Task SendUserPasswordRecoveryEmailAsync(User user, string passwordRecoveryToken, string encryptedUserId)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var emailTemplates = await GetActiveEmailTemplateAsync(MailTemplateDefaults.UserPasswordRecoveryMessage);
            if (emailTemplates == null)
                return;

            emailTemplates.Body = emailTemplates.Body.Replace("%Password.RecoveryToken%", passwordRecoveryToken)
                                                         .Replace("%User.Id%", encryptedUserId)
                                                         .Replace("%User.FirstName%", user.FirstName)
                                                         .Replace("%User.LastName%", user.LastName);

            await _emailSender.SendEmailAsync(emailTemplates.EmailAccount, emailTemplates.Subject, emailTemplates.Body, "Codebuddy", "info@codebuddy.com", user.Email, user.FirstName);

        }
    }
}
