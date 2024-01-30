using CleanMultitenant.Domain.Models.Shared;

namespace CleanMultitenant.Domain.Models.Internal
{
    public class Organization : BaseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SlugTenant { get; set; }
    }
}
