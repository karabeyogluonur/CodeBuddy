﻿using CB.Application.Abstractions.Services.Authentication;
using CB.Application.Abstractions.Services.Mail;
using CB.Domain.Entities.Membership;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CB.Services.Authentication
{
    public class RegistrationService : IRegistrationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IWorkflowMailService _workflowMailService;
        public RegistrationService(UserManager<AppUser> userManager, IWorkflowMailService workflowMailService)
        {
            _userManager = userManager;
            _workflowMailService = workflowMailService;
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
                await _workflowMailService.SendUserWelcomeMessageAsync(appUser);
            }
            return identityResult;

        }
    }
}