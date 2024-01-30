using CleanMultitenant.Application.Services.Products.AddProduct;
using CleanMultitenant.Application.Services.Products.DeleteProduct;
using CleanMultitenant.Application.Services.Products.GetProducts;
using CleanMultitenant.Application.Services.Products.UpdateProduct;
using CleanMultitenant.Domain.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanMultitenant.Api.Controllers
{
    [Route("api/{slugTenant}/[Controller]")]
    public class ProductController : MainController
    {
        public ProductController(INotifier notifier, IMediator mediator) : base(notifier, mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts(GetProductsRequest request)
        {
            var response = await mediator.Send(request);
            return CustomResponse(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductRequest request)
        {
            var response = await mediator.Send(request);
            return CustomResponse(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductRequest request)
        {
            var response = await mediator.Send(request);
            return CustomResponse(response);
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> AddProduct(Guid productId)
        {
            var response = await mediator.Send(new DeleteProductRequest(productId));
            return CustomResponse(response);
        }
    }
}
