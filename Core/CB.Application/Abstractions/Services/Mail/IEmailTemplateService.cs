using CB.Domain.Entities.Mail;

namespace CB.Application.Abstractions.Services.Mail
{
    public interface IEmailTemplateService
    {
        Task<EmailTemplate> GetEmailTemplatesByNameAsync(string emailTemplateName);
    }
}
