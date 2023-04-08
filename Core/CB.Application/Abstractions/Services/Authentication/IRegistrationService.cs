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
        Task<IdentityResult> RegisterAsync(AppUser appUser);
    }
}
