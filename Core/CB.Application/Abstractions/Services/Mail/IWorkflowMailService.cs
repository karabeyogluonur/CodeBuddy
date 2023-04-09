using CB.Domain.Entities.Membership;

namespace CB.Application.Abstractions.Services.Mail
{
    public interface IWorkflowMailService
    {
        Task SendUserWelcomeMessageAsync(AppUser user);
        Task SendUserEmailConfirmationMessageAsync(AppUser user, string emailConfirmationToken);
	}
}
