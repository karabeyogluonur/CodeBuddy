using CB.Domain.Entities.Mail;

namespace CB.Application.Abstractions.Services.Mail
{
    public interface IMailTemplateService
    {
        Task<MailTemplate> GetMailTemplatesByNameAsync(string MailTemplateName);
    }
}
