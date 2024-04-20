using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace Basket.Domain.UnitTests;

public class BasketTests
{
    private const string UserId = "10";

    [Test]
    public void  Basket_CreateBasket()
    {
        // Arrange
        var basket = new Basket.Domain.Entities.Basket();
        
        // Assert
        basket.Should().NotBeNull();
    }

    [Test]
    public void Basket_CreateAndAssignBasket()
    {
        // Arrange
        var basket = new Basket.Domain.Entities.Basket() 
        { 
            UserId = UserId
        };

        // Assert
        basket.Should().NotBeNull();
        basket.UserId.Should().Be(UserId);
        basket.Items.Should().NotBeNull();
    }

    [Test]
    public void Basket_AddItemToBasket()
    {
        // Arrange
        var basket = new Basket.Domain.Entities.Basket();
        var basketItem = new Basket.Domain.Entities.BasketItem()
        {
            ProductName = "Test Product",
            Price = 1.2m
        };

        // Act
        basket.Items.Add(basketItem);

        // Assert
        basket.Items.Should().NotBeNull();
        basket.Items.Count.Should().Be(1);
        basket.Items.First().Should().Be(basketItem);
    }
}
