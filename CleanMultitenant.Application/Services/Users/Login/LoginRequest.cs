using MediatR;

namespace CleanMultitenant.Application.Services.Users.Login
{
    public class LoginRequest : IRequest<LoginResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
