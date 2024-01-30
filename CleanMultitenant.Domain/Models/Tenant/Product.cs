using CleanMultitenant.Domain.Models.Shared;

namespace CleanMultitenant.Domain.Models.Tenant
{
    public class Product : BaseModel
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
