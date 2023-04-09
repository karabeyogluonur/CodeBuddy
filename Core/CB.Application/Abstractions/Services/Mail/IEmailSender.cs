using CB.Domain.Entities.Mail;

namespace CB.Application.Abstractions.Services.Mail
{
    public interface IEmailSender
    {
        Task SendEmailAsync(EmailAccount emailAccount, string subject, string body,
            string fromAddress, string fromName, string toAddress, string toName);
    }
}
