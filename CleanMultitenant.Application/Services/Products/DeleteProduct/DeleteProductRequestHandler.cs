using CleanMultitenant.Domain.Notifications;
using CleanMultitenant.Infra.Contexts.Tenant;

namespace CleanMultitenant.Application.Services.Products.DeleteProduct
{
    public class DeleteProductRequestHandler : RequestHandlerBase<DeleteProductRequest, bool>
    {
        private readonly ITenantDbContext _tenantDbContext;
        public DeleteProductRequestHandler(INotifier notifier, ITenantDbContext tenantDbContext) : base(notifier)
        {
            _tenantDbContext = tenantDbContext;
        }

        public override async Task<bool> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
        {
            var product = await _tenantDbContext.Products.FindAsync(request.Id);
            if (product == null)
            {
                Notify("No existe el producto");
                return false;
            }

            _tenantDbContext.Products.Remove(product);
            return await _tenantDbContext.SaveAsync();
        }
    }
}
