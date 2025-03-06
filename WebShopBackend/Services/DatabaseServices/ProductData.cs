using WebShopShared.Models;

namespace WebShopBackend.Services.DatabaseServices
{
	public static class ProductData
	{
		public static List<Product> GetSampleProducts()
		{
			List<Product> products = new List<Product>
			{
				new Product()
				{
					Name = "Lamborghini",
					Url = "https://images.unsplash.com/photo-1617718982887-3d73c4424f3a?q=80&w=1259&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
					Price = 2999999.99,
					Quantity = 1
				},
				new Product()
				{
					Name = "Chicken feet",
					Url = "https://images.unsplash.com/photo-1672787380739-6bd96bd86d4a?w=400&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTJ8fGNoaWNrZW4lMjBmZWV0fGVufDB8fDB8fHww",
					Price = 149,
					Quantity = 50
				},
				new Product()
				{
					Name = "Electric Scooter",
					Url = "https://images.unsplash.com/photo-1538895490524-0ded232a96d8?q=80&w=2130&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
					Price = 399.99,
					Quantity = 20
				},
				new Product()
				{
					Name = "Wireless Headphones",
					Url = "https://images.unsplash.com/photo-1599139894727-62676829679b?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
					Price = 89.99,
					Quantity = 100
				},
				new Product()
				{
					Name = "Leather Wallet",
					Url = "https://images.unsplash.com/photo-1620109176813-e91290f6c795?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
					Price = 29.99,
					Quantity = 150
				},
				new Product()
				{
					Name = "Smartwatch",
					Url = "https://images.unsplash.com/photo-1544117519-31a4b719223d?q=80&w=1952&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
					Price = 129.99,
					Quantity = 75
				},
				new Product()
				{
					Name = "Gaming Laptop",
					Url = "https://images.unsplash.com/photo-1623934199716-dc28818a6ec7?q=80&w=1932&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
					Price = 1299.99,
					Quantity = 10
				},
				new Product()
				{
					Name = "Frying Pan",
					Url = "https://images.unsplash.com/photo-1592156328697-079f6ee0cfa5?q=80&w=2080&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
					Price = 39.99,
					Quantity = 60
				},
				new Product()
				{
					Name = "Instant Camera",
					Url = "https://images.unsplash.com/photo-1599240211563-17590b1af857?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
					Price = 79.99,
					Quantity = 40
				},
				new Product()
				{
					Name = "Bluetooth Speaker",
					Url = "https://images.unsplash.com/photo-1578054041218-5ee0003926dd?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3",
					Price = 49.99,
					Quantity = 120
				},
				new Product()
				{
					Name = "Coffee Maker",
					Url = "https://images.unsplash.com/photo-1511001148140-09c2b155f57c?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
					Price = 99.99,
					Quantity = 85
				}
			};
			return products;
		}
	}
}
