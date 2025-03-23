using WebShopFrontend.Models;
using WebShopShared.Models;

namespace WebShopFrontend.Services
{
	public class OrderStateService
	{
		public List<OrderProductDetailsDto> Products { get; set; } = new();
		public PurchaseModel PurchaseModel { get; set; } = new();
		public double TotalCost { get; set; }
		public int OrderId { get; set; }
	}
}
