using CB.Application.Abstractions.Services.Authentication;
using CB.Application.Abstractions.Services.Mail;
using CB.Application.Abstractions.Services.Security;
using CB.Domain.Entities.Membership;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CB.Services.Authentication
{
    public class RegistrationService : IRegistrationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IWorkflowEmailService _workflowEmailService;
        private readonly IEncryptionService _encryptionService;
        public RegistrationService(UserManager<AppUser> userManager, IWorkflowEmailService workflowEmailService, IEncryptionService encryptionService)
        {
            _userManager = userManager;
            _workflowEmailService = workflowEmailService;
            _encryptionService = encryptionService;
        }
        public async Task<IdentityResult> RegisterAsync(AppUser appUser)
        {
            if (appUser == null)
                throw new ArgumentNullException(nameof(appUser));

            IdentityResult identityResult = await _userManager.CreateAsync(appUser, appUser.PasswordHash);
            if (identityResult.Succeeded)
            {
                appUser.Active = true;
                appUser.Deleted = false;
                await _userManager.UpdateAsync(appUser);
                await _workflowEmailService.SendUserWelcomeEmailAsync(appUser);
                await SendConfirmationAsync(appUser);
            }
            return identityResult;

        }

        public async Task<IdentityResult> EmailConfirmationAsync(AppUser appUser,string token)
        {
            if (appUser == null)
                throw new ArgumentNullException(nameof(appUser));

            if(String.IsNullOrEmpty(token))
                throw new ArgumentNullException(nameof(token));

            IdentityResult emailConfirmed = await _userManager.ConfirmEmailAsync(appUser, token.Replace(" ", "+"));
            return emailConfirmed;
        }

        public async Task SendConfirmationAsync(AppUser appUser)
        {
            if (appUser == null)
                throw new ArgumentNullException(nameof(appUser));

            string emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
            await _workflowEmailService.SendUserConfirmationEmailAsync(appUser, emailConfirmationToken);
        }

        public async Task SendPasswordRecoveryAsync(AppUser appUser)
        {
            if(appUser == null)
                throw new ArgumentNullException(nameof(appUser));

            var passwordRecoveryToken = await _userManager.GeneratePasswordResetTokenAsync(appUser);
            var encryptedUserId = _encryptionService.Encrypt(appUser.Id.ToString());
            await _workflowEmailService.SendUserPasswordRecoveryEmailAsync(appUser,passwordRecoveryToken,encryptedUserId);
        }

        public async Task<IdentityResult> PasswordRecoveryAsync(AppUser appUser,string token, string newPassword)
        {
            if (appUser == null)
                throw new ArgumentNullException(nameof(appUser));

            if(string.IsNullOrEmpty(newPassword))
                throw new ArgumentNullException(nameof(newPassword));

            if(string.IsNullOrEmpty(token)) 
                throw new ArgumentNullException(nameof(token));

            return await _userManager.ResetPasswordAsync(appUser,token.Replace(" ","+"),newPassword);
        }


    }
}
