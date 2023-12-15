﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Domain.Entities;
public class Basket
{
    public string UserId { get; set; }

    public List<BasketItem> Items { get; set; } = new List<BasketItem>();

    public Basket()
    {   
    }
}
