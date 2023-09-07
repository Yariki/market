using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection.Product.Commands.DeleteProduct;
using Microsoft.Extensions.DependencyInjection.Product.Commands.UpdateProduct;
using ProductCatalog.Application.Product.Commands.AddProduct;
using ProductCatalog.Application.Product.Models;
using ProductCatalog.WebUI.Controllers;
using ProductCatalog.WebUI.Filters;

namespace API.Controllers;

[ApiExceptionFilter]
[Route("api/v{version:apiVersion}/product")]
[ApiVersion("1.0")]
[Produces("application/json")]
public class ProductController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
    {
        return Ok(await Mediator.Send(new GetProductQuery()));
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> AddProduct([FromBody] AddProductCommand addProductCommand)
    {
        var guid = await Mediator.Send(addProductCommand);
        return Ok(guid);
    }

    [HttpPut("{productId:guid}")]
    public async Task<ActionResult> UpdateProduct(Guid productId, [FromBody] UpdateProductCommand updateProductCommand)
    {
        await Mediator.Send(updateProductCommand);
        return Ok();
    }

    [HttpDelete("{productId:guid}")]
    public async Task<ActionResult> DeleteProduct(Guid productId)
    {
        await Mediator.Send(new DeleteProductCommand() { ProductId = productId });

        return Ok();
    }
}
