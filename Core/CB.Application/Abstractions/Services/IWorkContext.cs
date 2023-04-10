using CB.Domain.Entities.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CB.Application.Abstractions.Services
{
    public interface IWorkContext
    {
        Task<AppUser> GetCurrentUserAsync();
    }
}
