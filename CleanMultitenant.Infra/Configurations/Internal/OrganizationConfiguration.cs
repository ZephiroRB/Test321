using CleanMultitenant.Domain.Models.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanMultitenant.Infra.Configurations.Internal
{
    public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.ToTable("Organizations");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Name).HasColumnType("VARCHAR");
            builder.Property(o => o.SlugTenant).HasColumnType("VARCHAR");
        }
    }
}
