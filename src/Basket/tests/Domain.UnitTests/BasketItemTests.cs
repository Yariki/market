using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basket.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace Basket.Domain.UnitTests
{
    public class BasketItemTests
    {
        
        [Test]
        public void BasketItem_CreateItem()
        {
            // Arrange
            var basketItem = new BasketItem();
            
            // Assert
            basketItem.Should().NotBeNull();
        }
        
        
        [Test]
        public void BasketItem_CreateAndAssign()
        {
            // Arrange
            
            var basketItem = new BasketItem()
            {
                ProductName = "Test Product",
                Price = 1.2m
            };

            // Assdert
            basketItem.ProductName.Should().Be("Test Product");
            basketItem.Price.Should().Be(1.2m);
        }
        
    }
}
