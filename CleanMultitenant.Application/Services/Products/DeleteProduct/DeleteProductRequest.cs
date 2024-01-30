using MediatR;

namespace CleanMultitenant.Application.Services.Products.DeleteProduct
{
    public class DeleteProductRequest : IRequest<bool>
    {
        public DeleteProductRequest(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
