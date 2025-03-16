namespace WebShopBackend.Models
{
	public class Product
	{
		public int Id { get; set; }
		public required string Name { get; set; }
		public double Price { get; set; }
		public required string Url { get; set; }
		public int Quantity { get; set; }
		public bool IsInStock => Quantity > 0;
		public List<OrderProduct> OrderProducts { get; set; } = new();
	}
}
