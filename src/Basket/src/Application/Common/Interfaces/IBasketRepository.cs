namespace Basket.Application.Common.Interfaces;

public interface IBasketRepository
{
    Task<Domain.Entities.Basket?> GetBasketAsync(string customerId);
    Task<Domain.Entities.Basket?> UpdateBasketAsync(Domain.Entities.Basket basket);
    Task DeleteBasketAsync(string customerId);
}