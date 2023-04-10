﻿using CB.Application.Abstractions.Services.Authentication;
using CB.Domain.Entities.Membership;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CB.Services.Authentication
{
    public class LoginService : ILoginService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginService(SignInManager<AppUser> signInManager, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<SignInResult> SignInAsync(AppUser appUser, string password, bool rememberMe)
        {
            SignInResult signInResult = await _signInManager.PasswordSignInAsync(appUser, password, rememberMe, true);
            if (signInResult.Succeeded)              
                await _userManager.ResetAccessFailedCountAsync(appUser);

            return signInResult;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
