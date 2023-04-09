using CB.Domain.Entities.Membership;

namespace CB.Application.Abstractions.Services.Mail
{
    public interface IWorkflowEmailService
    {
        Task SendUserWelcomeEmailAsync(AppUser user);
        Task SendUserConfirmationEmailAsync(AppUser user, string emailConfirmationToken);
	}
}
