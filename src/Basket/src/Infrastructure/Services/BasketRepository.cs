using Basket.Application.Common.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.Infrastructure.Services;

public class BasketRepository : IBasketRepository
{
    private readonly IDistributedCache _cache;
    
    
    public BasketRepository(IDistributedCache cache)
    {
        _cache = cache;
    }
    
    public async Task<Domain.Entities.Basket?> GetBasketAsync(string customerId)
    {
        var basket = await _cache.GetStringAsync(customerId);
        return string.IsNullOrEmpty(basket) ? null : JsonConvert.DeserializeObject<Domain.Entities.Basket>(basket);
    }
    
    public async Task<Domain.Entities.Basket?> UpdateBasketAsync(Domain.Entities.Basket basket)
    {
        await _cache.SetStringAsync(basket.UserId, JsonConvert.SerializeObject(basket));
        return await GetBasketAsync(basket.UserId);
    }
    
    public async Task DeleteBasketAsync(string customerId)
    {
        await _cache.RemoveAsync(customerId);
    }
    
}