using CleanMultitenant.Domain.Notifications;
using CleanMultitenant.Infra.Contexts.Tenant;
using Microsoft.EntityFrameworkCore;

namespace CleanMultitenant.Application.Services.Products.GetProducts
{
    public class GetProductsRequestHandler : RequestHandlerBase<GetProductsRequest, IEnumerable<GetProductsResponse>>
    {
        private readonly ITenantDbContext _tenantDbContext;
        public GetProductsRequestHandler(INotifier notifier, ITenantDbContext tenantDbContext) : base(notifier)
        {
            _tenantDbContext = tenantDbContext;
        }

        public override async Task<IEnumerable<GetProductsResponse>> Handle(GetProductsRequest request, CancellationToken cancellationToken)
        {
            var products = await _tenantDbContext.Products.ToListAsync();
            return products.Select(p => new GetProductsResponse
            {
                Id = p.Id,
                Name = p.Name,
                ImageUrl = p.ImageUrl,
                Description = p.Description,
                Price = p.Price
            });
        }
    }
}
