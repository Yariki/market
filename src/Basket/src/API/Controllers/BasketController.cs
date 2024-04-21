using Basket.Api.Controllers;
using Basket.Application.Basket.Commands;
using Basket.Application.Basket.Models;
using Basket.Application.Basket.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers;


public class BasketController : ApiControllerBase
{
    [HttpGet]
    [Route("{customerId}")]
    public async Task<ActionResult<BasketDto>> GetBasketById(string customerId)
    {
        var basket = await Mediator.Send(new GetBasketQuery() 
        { 
            CustomerId = customerId
        });
        return Ok(basket);
    }

    [HttpPost]
    public async Task<ActionResult<BasketDto>> UpdateBasket([FromBody] BasketDto basket)
    {
        var basketResult = await Mediator.Send(new UpdateBasketCommand() 
        { 
            UserId = Guid.Parse(basket.UserId),
            Basket = basket
        } );
        return Ok(basketResult);
    }

    [HttpDelete]
    [Route("{customerId}")]
    public async Task<ActionResult> DeleteBasket(string customerId)
    {
        await Mediator.Send(new DeleteBasketCommand() 
        { 
            CustomerId = customerId
        });
        return NoContent();
    }
}
