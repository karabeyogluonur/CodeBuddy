using CB.Application.Abstractions.Services.Membership;
using CB.Domain.Entities.Membership;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CB.Services.Membership
{
    public class UserService : IUserService
    {
        UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AppUser> GetAvaibleUserByEmailAsync(string email)
        {
            return await _userManager.Users.FirstOrDefaultAsync(predicate: user => user.Active && !user.Deleted && user.Email == email);
        }

        public async Task<AppUser> GetAvaibleUserByIdAsync(int id)
        {
            return await _userManager.Users.FirstOrDefaultAsync(predicate: user => user.Active && !user.Deleted && user.Id == id);
        }
    }
}
