using CB.Application.Abstractions.Services.Authentication;
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
            {
                await _userManager.ResetAccessFailedCountAsync(appUser);
                var userRoles = await _userManager.GetRolesAsync(appUser);
                List<Claim> userClaims = new List<Claim>();
                if (userRoles.Any())
                {        
                    foreach (var role in userRoles)
                    {
                        userClaims.Add(new Claim(type: ClaimTypes.Role, role));
                    }                   
                }
                userClaims.Add(new(type:ClaimTypes.NameIdentifier,appUser.UserName));
                userClaims.Add(new(type:ClaimTypes.Email,appUser.Email));
                userClaims.Add(new(type:ClaimTypes.Name,appUser.FirstName + " " + appUser.LastName));
                await _userManager.AddClaimsAsync(appUser, userClaims);
            }              
            return signInResult;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
