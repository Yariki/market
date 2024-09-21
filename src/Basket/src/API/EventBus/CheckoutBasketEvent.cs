using Basket.Application.Basket.Models;
using Market.EventBus;

namespace Microsoft.Extensions.DependencyInjection.EventBus;

public class CheckoutBasketEvent : BaseIntegrationEvent
{

    public CheckoutBasketEvent() 
        : base()
    {
    }
    
    public CheckoutBasketEvent(string correlationId, BasketDto basket) : base(correlationId)
    {
        Basket = basket;
    }
    
    public CheckoutBasketEvent(BasketDto basket) : this()
    {
        Basket = basket;
    }
    
    public BasketDto Basket { get; init; }   
}