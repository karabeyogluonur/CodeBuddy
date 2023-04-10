﻿using CB.Domain.Entities.Membership;
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
        Task<IdentityResult> RegisterAsync(AppUser appUser);
        Task<IdentityResult> EmailConfirmationAsync(AppUser appUser, string token);
        Task SendConfirmationAsync(AppUser appUser);
        Task SendPasswordRecoveryAsync(AppUser appUser);
        Task<IdentityResult> PasswordRecoveryAsync(AppUser appUser, string token, string newPassword);
    }
}
