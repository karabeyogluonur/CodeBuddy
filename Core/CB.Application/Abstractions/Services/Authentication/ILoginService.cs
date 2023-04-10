﻿using CB.Domain.Entities.Membership;
using Microsoft.AspNetCore.Identity;

namespace CB.Application.Abstractions.Services.Authentication
{
    public interface ILoginService
    {
        Task<SignInResult> SignInAsync(AppUser appUser, string password, bool rememberMe);
        Task SignOutAsync();
    }
}
