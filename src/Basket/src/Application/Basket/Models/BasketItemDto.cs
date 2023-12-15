using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basket.Application.Common.Mappings;

namespace Basket.Application.Basket.Models;
public class BasketItemDto :  IMapFrom<Domain.Entities.BasketItem>
{
    public Guid ProductId { get; set; }

    public string ProductName { get; set; }

    public Guid SellUnitId { get; set; }

    public string SellUnitName { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public DateTime CreatedAt { get; set; }

    public string ProductPhotoUrl { get; set; }
}
