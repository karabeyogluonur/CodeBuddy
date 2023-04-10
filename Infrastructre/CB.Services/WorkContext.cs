using CB.Application.Abstractions.Services;
using CB.Domain.Entities.Membership;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CB.Services
{
    public class WorkContext : IWorkContext
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WorkContext(UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AppUser> GetCurrentUserAsync()
        {
            ClaimsPrincipal userContext = _httpContextAccessor.HttpContext.User;
            if (userContext == null)
                return null;
            else
                return await _userManager.GetUserAsync(userContext);
        }
    }
}
