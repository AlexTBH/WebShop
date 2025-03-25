using WebShopBackend.Models;

namespace WebShopBackend.Models
{
	public class Order
	{
		public int Id { get; set; }
		public required string UserId { get; set; }
		public OrderStatus Status { get; set; } = OrderStatus.Pending;
		public List<OrderProduct> OrderProducts { get; set; } = new();
 	}

	public enum OrderStatus
	{
		Pending,
		Completed
	}
}
