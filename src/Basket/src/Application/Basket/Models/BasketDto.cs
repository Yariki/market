using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basket.Application.Common.Mappings;
using Basket.Domain.Entities;

namespace Basket.Application.Basket.Models;
public class BasketDto : IMapFrom<Domain.Entities.Basket>
{
    public string UserId { get; set; }

    public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();

}
