using CleanMultitenant.Domain.Models.Tenant;
using Microsoft.EntityFrameworkCore;

namespace CleanMultitenant.Infra.Contexts.Tenant
{
    public interface ITenantDbContext
    {
        DbSet<Product> Products { get; set; }
        Task<bool> SaveAsync(CancellationToken cancellationToken = default);
    }
}
