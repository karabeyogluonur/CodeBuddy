using Microsoft.AspNetCore.Identity;

namespace CB.Domain.Entities.Membership
{
    public class AppUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool Verified { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
    }
}
