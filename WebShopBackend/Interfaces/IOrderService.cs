using WebShopBackend.Models;
using WebShopShared.Models;

namespace WebShopBackend.Interfaces
{
	public interface IOrderService
	{
		public Task PostOrderProduct(AddToCartDto productId, string userEmail);
		
	}
}
