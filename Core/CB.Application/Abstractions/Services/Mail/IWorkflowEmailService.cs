using CB.Domain.Entities.Membership;

namespace CB.Application.Abstractions.Services.Mail
{
    public interface IWorkflowEmailService
    {
        Task SendUserWelcomeEmailAsync(User user);
        Task SendUserConfirmationEmailAsync(User user, string emailConfirmationToken);
        Task SendUserPasswordRecoveryEmailAsync(User user, string passwordRecoveryToken, string encryptedUserId);

    }
}
