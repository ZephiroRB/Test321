using CleanMultitenant.Domain.Models.Internal;

namespace CleanMultitenant.Infra.Services.Security
{
    public interface ISecurityService
    {
        string GenerateToken(User user);
        string GetTenant();
    }
}
