using WebShopShared;

namespace WebShopBackend.Services
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
					Url = "https://images.unsplash.com/photo-1579040807030-f1e6c67be624?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
					Price = 399.99,
					Quantity = 20
				},
				new Product()
				{
					Name = "Wireless Headphones",
					Url = "https://images.unsplash.com/photo-1570412236925-6a481990b2c5?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
					Price = 89.99,
					Quantity = 100
				},
				new Product()
				{
					Name = "Leather Wallet",
					Url = "https://images.unsplash.com/photo-1501741865154-b7a5087032da?w=400&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
					Price = 29.99,
					Quantity = 150
				},
				new Product()
				{
					Name = "Smartwatch",
					Url = "https://images.unsplash.com/photo-1578427226235-b22b32e8f5d3?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
					Price = 129.99,
					Quantity = 75
				},
				new Product()
				{
					Name = "Gaming Laptop",
					Url = "https://images.unsplash.com/photo-1611049992569-0548ad8e7cfa?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
					Price = 1299.99,
					Quantity = 10
				},
				new Product()
				{
					Name = "Frying Pan",
					Url = "https://images.unsplash.com/photo-1571176638262-e699d4f0f8fe?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
					Price = 39.99,
					Quantity = 60
				},
				new Product()
				{
					Name = "Instant Camera",
					Url = "https://images.unsplash.com/photo-1565073555-d87985fc191f?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
					Price = 79.99,
					Quantity = 40
				},
				new Product()
				{
					Name = "Bluetooth Speaker",
					Url = "https://images.unsplash.com/photo-1565309222-df6b929906d3?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
					Price = 49.99,
					Quantity = 120
				},
				new Product()
				{
					Name = "Coffee Maker",
					Url = "https://images.unsplash.com/photo-1591294202607-b8cd3ecf62b3?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
					Price = 99.99,
					Quantity = 85
				}
				return products;
			};
		}
	}
}
