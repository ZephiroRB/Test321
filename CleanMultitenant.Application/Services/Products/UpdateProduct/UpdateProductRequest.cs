using MediatR;

namespace CleanMultitenant.Application.Services.Products.UpdateProduct
{
    public class UpdateProductRequest : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
