using Basket.Application.Basket.Commands;
using Basket.Application.Basket.Models;
using Basket.Application.Common.Exceptions;
using FluentAssertions;
using NUnit.Framework;


namespace Basket.Application.IntegrationTests.Basket.Commands;

using static Testing;

public class UpdateBasketCommandTests : BaseTestFixture
{
    [Test]
    public async Task UpdateBasket_ValidationCheck()
    {
        var command = new UpdateBasketCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldAddBasket()
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

        var basketId = await SendAsync(command);

        var basket = await FindAsync(CurrentUserId);

        basket.Should().NotBeNull();
        basket.UserId.Should().Be(CurrentUserId);
        basket.Items.Count.Should().Be(1);
    }

    [Test]
    public async Task ShouldUpdateBasket()
    {

        var productId = Guid.NewGuid();
        var testproduct = "TestProduct";

        var basket = new BasketDto()
        {
            UserId = CurrentUserId,
            Items = new List<BasketItemDto>()
                {
                    new BasketItemDto()
                    {
                        ProductId = productId,
                        Price = 10,
                        ProductName = testproduct,
                        Quantity = 2
                    }
                }
        };

        var command = new UpdateBasketCommand
        {
            UserId = Guid.Parse(CurrentUserId),
            Basket = basket
        };

        var temp = await SendAsync(command);

        var basketResult = await FindAsync(CurrentUserId);

        basketResult.Should().NotBeNull();
        basketResult.UserId.Should().Be(CurrentUserId);
        basketResult.Items.Count.Should().Be(1);
        basketResult.Items.First().Quantity.Should().Be(2);


        basket.Items.First().Quantity = 3;

        command = new UpdateBasketCommand
        {
            UserId = Guid.Parse(CurrentUserId),
            Basket = basket
        };

        temp = await SendAsync(command);

        basketResult = await FindAsync(CurrentUserId);

        basketResult.Should().NotBeNull();
        basketResult.UserId.Should().Be(CurrentUserId);
        basketResult.Items.Count.Should().Be(1);
        basketResult.Items.First().Quantity.Should().Be(3);

    }

}
