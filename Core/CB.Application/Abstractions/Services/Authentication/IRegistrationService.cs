using CB.Domain.Entities.Membership;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CB.Application.Abstractions.Services.Authentication
{
    public interface IRegistrationService
    {
        Task<IdentityResult> RegisterAsync(User user);
        Task<IdentityResult> EmailConfirmationAsync(User user, string token);
        Task SendConfirmationAsync(User user);
        Task SendPasswordRecoveryAsync(User user);
        Task<IdentityResult> PasswordRecoveryAsync(User user, string token, string newPassword);
    }
}
