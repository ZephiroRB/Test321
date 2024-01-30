using CleanMultitenant.Domain.Models.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanMultitenant.Infra.Seeds
{
    public class OrganizationSeed : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.HasData(
                new Organization() { Id = new Guid("4e1894dc-8bd4-44da-b9b2-08c715700b1e"), Name = "Tenant1", SlugTenant = "Tenant1" },
                new Organization() { Id = new Guid("afa271ff-67ad-4a96-9f7d-828a368bef1b"), Name = "Tenant2", SlugTenant = "Tenant2" }
            );
        }
    }
}
