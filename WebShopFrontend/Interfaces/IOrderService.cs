using WebShopShared.Models;

namespace WebShopFrontend.Interfaces
{
	public interface IOrderService
	{
		public Task AddToCart(AddToCartDto productId);
	}
}
