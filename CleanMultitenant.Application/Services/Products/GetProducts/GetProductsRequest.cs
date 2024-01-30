using MediatR;

namespace CleanMultitenant.Application.Services.Products.GetProducts
{
    public class GetProductsRequest : IRequest<IEnumerable<GetProductsResponse>>
    {
    }
}
