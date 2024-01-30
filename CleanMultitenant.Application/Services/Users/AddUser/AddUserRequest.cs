using MediatR;

namespace CleanMultitenant.Application.Services.Users.AddUser
{
    public class AddUserRequest : IRequest<bool>
    {
        public AddUserRequest(Guid organizationId, string email, string password)
        {
            OrganizationId = organizationId;
            Email = email;
            Password = password;
        }

        public Guid OrganizationId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
