using CleanMultitenant.Domain.Models.Internal;
using CleanMultitenant.Infra.Configurations.Internal;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CleanMultitenant.Infra.Contexts.Internal
{
    public class InternalDbContext : IdentityDbContext<User>
    {
        public InternalDbContext(DbContextOptions<InternalDbContext> options) : base(options)
        {
        }

        public DbSet<Organization> Organizations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new OrganizationConfiguration());
        }
    }
}
