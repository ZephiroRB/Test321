using CleanMultitenant.Domain.Models.Tenant;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanMultitenant.Infra.Configurations.Tenant
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).HasColumnType("VARCHAR");
            builder.Property(p => p.ImageUrl).HasColumnType("VARCHAR");
            builder.Property(p => p.Description).HasColumnType("VARCHAR");
            builder.Property(p => p.Price).HasColumnType("DECIMAL(10, 2)");
        }
    }
}
