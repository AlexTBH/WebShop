using System.Diagnostics.Contracts;
using WebShopShared.Models;

namespace WebShopFrontend.Interfaces
{
	public interface IOrderService
	{
		public Task AddToCart(AddToCartDto productId);
		public Task<List<OrderProductDetailsDto>> GetOrderProducts();
		public Task<int> GetOrderId();
		public Task ChangeOrderStatus(int id);
	}
}
