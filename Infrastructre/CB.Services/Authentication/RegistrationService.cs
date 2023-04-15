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
        private readonly UserManager<User> _userManager;
        private readonly IWorkflowEmailService _workflowEmailService;
        private readonly IEncryptionService _encryptionService;
        public RegistrationService(UserManager<User> userManager, IWorkflowEmailService workflowEmailService, IEncryptionService encryptionService)
        {
            _userManager = userManager;
            _workflowEmailService = workflowEmailService;
            _encryptionService = encryptionService;
        }
        public async Task<IdentityResult> RegisterAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            IdentityResult identityResult = await _userManager.CreateAsync(user,user.PasswordHash);
            if (identityResult.Succeeded)
            {
                user.Active = true;
                user.Deleted = false;
                await _userManager.UpdateAsync(user);
                await _userManager.AddToRoleAsync(user, "Member");
                await _workflowEmailService.SendUserWelcomeEmailAsync(user);
                await SendConfirmationAsync(user);
            }
            return identityResult;

        }

        public async Task<IdentityResult> EmailConfirmationAsync(User user,string token)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if(String.IsNullOrEmpty(token))
                throw new ArgumentNullException(nameof(token));

            IdentityResult emailConfirmed = await _userManager.ConfirmEmailAsync(user, token.Replace(" ", "+"));
            return emailConfirmed;
        }

        public async Task SendConfirmationAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            string emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            await _workflowEmailService.SendUserConfirmationEmailAsync(user, emailConfirmationToken);
        }

        public async Task SendPasswordRecoveryAsync(User user)
        {
            if(user == null)
                throw new ArgumentNullException(nameof(user));

            var passwordRecoveryToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encryptedUserId = _encryptionService.Encrypt(user.Id.ToString());
            await _workflowEmailService.SendUserPasswordRecoveryEmailAsync(user,passwordRecoveryToken,encryptedUserId);
        }

        public async Task<IdentityResult> PasswordRecoveryAsync(User user,string token, string newPassword)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if(string.IsNullOrEmpty(newPassword))
                throw new ArgumentNullException(nameof(newPassword));

            if(string.IsNullOrEmpty(token)) 
                throw new ArgumentNullException(nameof(token));

            return await _userManager.ResetPasswordAsync(user,token.Replace(" ","+"),newPassword);
        }


    }
}
