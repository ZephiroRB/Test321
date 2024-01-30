using CleanMultitenant.Domain.Notifications;
using MediatR;

namespace CleanMultitenant.Application.Services
{
    public abstract class HandlerBase
    {
    }

    public abstract class RequestHandlerBase<TRequest, TResponse>: HandlerBase, IRequestHandler<TRequest, TResponse> where TRequest: IRequest<TResponse>
    {
        protected INotifier _notifier;

        protected RequestHandlerBase(INotifier notifier)
        {
            _notifier = notifier;
        }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);

        protected void Notify(string message)
        {
            _notifier.Handle(message);
        }
    }
}
