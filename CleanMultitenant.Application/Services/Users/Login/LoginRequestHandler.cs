using CleanMultitenant.Domain.Interfaces.Internal;
using CleanMultitenant.Domain.Models.Internal;
using CleanMultitenant.Domain.Notifications;
using CleanMultitenant.Infra.Services.Security;
using Microsoft.AspNetCore.Identity;

namespace CleanMultitenant.Application.Services.Users.Login
{
    public class LoginRequestHandler : RequestHandlerBase<LoginRequest, LoginResponse>
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly UserManager<User> _userManager;
        private readonly ISecurityService _securityService;
        private readonly SignInManager<User> _signInManager;

        public LoginRequestHandler(INotifier notifier, IOrganizationRepository organizationRepository, UserManager<User> userManager, ISecurityService securityService, SignInManager<User> signInManager) : base(notifier)
        {
            _organizationRepository = organizationRepository;
            _userManager = userManager;
            _securityService = securityService;
            _signInManager = signInManager;
        }

        public override async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var identityUser = await _userManager.FindByEmailAsync(request.Email);
            if (identityUser == null)
            {
                Notify("No se encontró al usuario");
                return new LoginResponse();
            }

            var response = await _signInManager.PasswordSignInAsync(user: identityUser, password: request.Password, isPersistent: true, lockoutOnFailure: false);
            if (!response.Succeeded)
            {
                Notify("Los datos ingresados son incorrectos");
                return new LoginResponse();
            }

            var organization = await _organizationRepository.FirstAsync(x => x.Id == identityUser.OrganizationId);
            var accessToken = _securityService.GenerateToken(identityUser);
            return new LoginResponse
            {
                AccessToken = accessToken,
                Tenant = organization.SlugTenant
            };
        }
    }
}
