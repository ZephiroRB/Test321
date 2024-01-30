using CleanMultitenant.Domain.Interfaces.Internal;
using CleanMultitenant.Domain.Models.Internal;
using CleanMultitenant.Domain.Notifications;
using Microsoft.AspNetCore.Identity;

namespace CleanMultitenant.Application.Services.Users.AddUser
{
    public class AddUserRequestHandler : RequestHandlerBase<AddUserRequest, bool>
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly UserManager<User> _userManager;

        public AddUserRequestHandler(INotifier notifier, IOrganizationRepository organizationRepository, UserManager<User> userManager) : base(notifier)
        {
            _organizationRepository = organizationRepository;
            _userManager = userManager;
        }

        public override async Task<bool> Handle(AddUserRequest request, CancellationToken cancellationToken)
        {
            var organization = await _organizationRepository.FirstAsync(o => o.Id == request.OrganizationId);
            if (organization == null)
            {
                Notify("No existe la organización");
                return false;
            }

            var identityUser = new User(request.Email)
            {
                Email = request.Email,
                EmailConfirmed = true,
                OrganizationId = request.OrganizationId,
                PhoneNumberConfirmed = true
            };
            var response = await _userManager.CreateAsync(identityUser, request.Password);

            if (!response.Succeeded)
            {
                Notify("No se genero el usuario");
                return false;
            }

            return true;
        }
    }
}
