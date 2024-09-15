using Basket.Api.Controllers;
using Basket.Application.Basket.Commands;
using Basket.Application.Basket.Models;
using Basket.Application.Basket.Queries;
using Market.EventBus;
using Market.Shared.CorrelationId;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.EventBus;

namespace Basket.API.Controllers;


//[Authorize(Policy = "basket-api")]
public class BasketController : ApiControllerBase
{
    public readonly IEventSender _eventSender;
    public readonly ILogger<BasketController> _logger;
    public readonly ICorrelationIdService _correlationIdService;

    public BasketController(IEventSender eventSender,
        ILogger<BasketController> logger, ICorrelationIdService correlationIdService)
    {
        _eventSender = eventSender;
        _logger = logger;
        _correlationIdService = correlationIdService;
    }

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
    
    [HttpGet]
    [Route("checkout/{customerId}")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> CheckoutBasket(string customerId)
    {
        try
        {
            var basket = await Mediator.Send(new GetBasketQuery() 
            { 
                CustomerId = customerId
            });
            if (basket == null)
            {
                return NotFound();
            }

            var busEvent = new CheckoutBasketEvent(_correlationIdService.Get(), basket);
            var result = await _eventSender.SendAsync( busEvent);
            
            _logger.LogInformation($"The event with Id={busEvent.Id} and CorrelationId={busEvent.CorrelationId} has been published. Result: {result}");

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
}
