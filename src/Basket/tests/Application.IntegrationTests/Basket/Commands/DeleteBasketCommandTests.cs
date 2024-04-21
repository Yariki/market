using Basket.Application.Basket.Commands;
using Basket.Application.Basket.Models;
using Basket.Application.Common.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace Basket.Application.IntegrationTests.Basket.Commands;

using static Testing;

public class DeleteBasketCommandTests : BaseTestFixture
{
    [Test]
    public async Task DeleteBasket_ValidationCheck()
    {
        var command = new DeleteBasketCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldDeleteBasket()
    {
        var productId = Guid.NewGuid();
        var testproduct = "TestProduct";
        
        var command = new UpdateBasketCommand
        {
            UserId = Guid.Parse(CurrentUserId),
            Basket = new BasketDto()
            {
                UserId = CurrentUserId,
                Items = new List<BasketItemDto>()
                {
                    new BasketItemDto()
                    {
                        ProductId = productId,
                        Price = 10,
                        ProductName = testproduct
                    }
                }
            }
        };

        var result = await SendAsync(command);

        var basket = await FindAsync(CurrentUserId);

        basket.Should().NotBeNull();
        basket.UserId.Should().Be(CurrentUserId);
        basket.Items.Count.Should().Be(1);

        var deleteCommand = new DeleteBasketCommand
        {
            CustomerId = CurrentUserId
        };

        var deleteResult = await SendAsync(deleteCommand);
        basket = await FindAsync(CurrentUserId);

        basket.Should().BeNull();
    }

}
