﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopShared.Models
{
    public class AddToCartDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
