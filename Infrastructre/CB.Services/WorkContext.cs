using CB.Application.Abstractions.Services;
using CB.Domain.Entities.Membership;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CB.Services
{
    public class WorkContext : IWorkContext
    {
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WorkContext(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<User> GetCurrentUserAsync()
        {
            ClaimsPrincipal userContext = _httpContextAccessor.HttpContext.User;
            if (userContext == null)
                return null;
            else
                return await _userManager.GetUserAsync(userContext);
        }
    }
}
