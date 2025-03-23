using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopShared.Models;

namespace WebShopShared.Models
{
    public class OrderProductDetailsDto
    {
        public ProductDto Product { get; set; }
        public int Quantity { get; set; }
    }
}
