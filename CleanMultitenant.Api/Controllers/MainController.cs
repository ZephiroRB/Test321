using CleanMultitenant.Domain.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanMultitenant.Api.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly INotifier notifier;
        protected IMediator mediator;

        protected MainController(INotifier notifier, IMediator mediator)
        {
            this.notifier = notifier;
            this.mediator = mediator;
        }

        protected ActionResult CustomResponse(object? response)
        {
            var messages = notifier.GetNotifications().Select(n => n.Message);

            if (notifier.HasNotification())
                return BadRequest(messages);

            return Ok(response);
        }
    }
}
