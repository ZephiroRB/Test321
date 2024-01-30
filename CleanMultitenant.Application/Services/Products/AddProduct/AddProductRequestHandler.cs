using CleanMultitenant.Domain.Models.Tenant;
using CleanMultitenant.Domain.Notifications;
using CleanMultitenant.Infra.Contexts.Tenant;

namespace CleanMultitenant.Application.Services.Products.AddProduct
{
    public class AddProductRequestHandler : RequestHandlerBase<AddProductRequest, bool>
    {
        private readonly ITenantDbContext _tenantDbContext;

        public AddProductRequestHandler(INotifier notifier, ITenantDbContext tenantDbContext) : base(notifier)
        {
            _tenantDbContext = tenantDbContext;
        }

        public override async Task<bool> Handle(AddProductRequest request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                ImageUrl = request.ImageUrl,
                Description = request.Description,
                Price = request.Price
            };

            _tenantDbContext.Products.Add(product);
            return  _tenantDbContext.SaveAsync(); 
        }
    }
}
