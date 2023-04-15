using CB.Domain.Entities.Membership;
using Microsoft.AspNetCore.Identity;

namespace CB.Application.Abstractions.Services.Authentication
{
    public interface ILoginService
    {
        Task<SignInResult> SignInAsync(User user, string password, bool rememberMe);
        Task SignOutAsync();
    }
}
