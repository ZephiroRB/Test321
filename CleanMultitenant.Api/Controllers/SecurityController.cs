using CleanMultitenant.Application.Services.Users.AddUser;
using CleanMultitenant.Application.Services.Users.Login;
using CleanMultitenant.Domain.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanMultitenant.Api.Controllers
{
    [Route("api/[Controller]")]
    public class SecurityController : MainController
    {
        public SecurityController(INotifier notifier, IMediator mediator) : base(notifier, mediator)
        {
        }

        [HttpPost("register-user")]
        public async Task<IActionResult> RegisterUser(AddUserRequest request)
        {
            var response = await mediator.Send(request);
            return CustomResponse(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var response = await mediator.Send(request);
            return CustomResponse(response);
        }
    }
}
