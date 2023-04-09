using CB.Domain.Entities.Mail;

namespace CB.Application.Abstractions.Services.Mail
{
    public interface IEmailAccountService
    {
        Task<EmailAccount> GetEmailAccountByIdAsync(int emailAccountId);
        Task<EmailAccount> GetDefaultEmailAccountAsync();
    }
}
