using CB.Domain.Entities.Membership;

namespace CB.Application.Abstractions.Services.Membership
{
    public interface IUserService
    {
        Task<User> GetAvaibleUserByIdAsync(int id);
        Task<User> GetAvaibleUserByEmailAsync(string email);
    }
}
