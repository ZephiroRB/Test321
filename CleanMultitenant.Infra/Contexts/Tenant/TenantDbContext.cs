using CleanMultitenant.Domain.Models.Tenant;
using CleanMultitenant.Infra.Configurations.Tenant;
using CleanMultitenant.Infra.Services.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CleanMultitenant.Infra.Contexts.Tenant
{
    public class TenantDbContext : DbContext, ITenantDbContext
    {
        private readonly ISecurityService _securityService;
        private readonly IConfiguration _configuration;

        public TenantDbContext(ISecurityService securityService, IConfiguration configuration)
        {
            _securityService = securityService;
            _configuration = configuration;
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var tenant = _securityService.GetTenant();
            var connectionString = _configuration.GetConnectionString(tenant);
            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
        }

        public async Task<bool> SaveAsync(CancellationToken cancellationToken = default)
        {
            var response =  base.SaveChangesAsync(cancellationToken);
            return response > 0;
        }
    }
}
