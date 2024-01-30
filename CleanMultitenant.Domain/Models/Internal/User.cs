using Microsoft.AspNetCore.Identity;

namespace CleanMultitenant.Domain.Models.Internal
{
    public class User : IdentityUser
    {
        public User(string userName) : base(userName)
        {
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public Guid OrganizationId { get; set; }
    }
}
