using CleanMultitenant.Domain.Interfaces.Internal;
using CleanMultitenant.Domain.Models.Internal;
using CleanMultitenant.Infra.Contexts.Internal;

namespace CleanMultitenant.Infra.Repositories.Internal
{
    public class OrganizationRepository : GenericRepository<InternalDbContext, Organization>, IOrganizationRepository
    {
        public OrganizationRepository(InternalDbContext context) : base(context)
        {
        }
    }
}
