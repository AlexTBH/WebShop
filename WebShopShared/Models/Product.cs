using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopShared.Models
{
    public class Product
    {
        public int id { get; set; }
        public required string Name { get; set; }
        public double Price { get; set; }
        public required string Url { get; set; }
        public int Quantity { get; set; }
    }

}
