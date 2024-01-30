namespace CleanMultitenant.Application.Services.Products.GetProducts
{
    public class GetProductsResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
