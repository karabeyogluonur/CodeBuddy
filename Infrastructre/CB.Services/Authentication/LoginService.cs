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
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginService(SignInManager<User> signInManager, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<SignInResult> SignInAsync(User user, string password, bool rememberMe)
        {
            SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, password, rememberMe, true);
            if (signInResult.Succeeded)
            {
                await _userManager.ResetAccessFailedCountAsync(user);
                var userRoles = await _userManager.GetRolesAsync(user);
                List<Claim> userClaims = new List<Claim>();
                if (userRoles.Any())
                {        
                    foreach (var role in userRoles)
                    {
                        userClaims.Add(new Claim(type: ClaimTypes.Role, role));
                    }                   
                }
                userClaims.Add(new(type:ClaimTypes.NameIdentifier,user.UserName));
                userClaims.Add(new(type:ClaimTypes.Email,user.Email));
                userClaims.Add(new(type:ClaimTypes.Name,user.FirstName + " " + user.LastName));
                await _userManager.AddClaimsAsync(user, userClaims);
            }              
            return signInResult;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
