using CleanMultitenant.Domain.Notifications;
using CleanMultitenant.Infra.Contexts.Tenant;

namespace CleanMultitenant.Application.Services.Products.UpdateProduct
{
    public class UpdateProductRequestHandler : RequestHandlerBase<UpdateProductRequest, bool>
    {
        private readonly ITenantDbContext _tenantDbContext;

        public UpdateProductRequestHandler(INotifier notifier, ITenantDbContext tenantDbContext) : base(notifier)
        {
            _tenantDbContext = tenantDbContext;
        }

        public override async Task<bool> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
        {
            var product = await _tenantDbContext.Products.FindAsync(request.Id);
            if(product == null)
            {
                Notify("No existe el producto");
                return false;
            }

            product.Name = request.Name;
            product.ImageUrl = request.ImageUrl;
            product.Description = request.Description;
            product.Price = request.Price;

            _tenantDbContext.Products.Update(product);
            return await _tenantDbContext.SaveAsync();
        }
    }
}
