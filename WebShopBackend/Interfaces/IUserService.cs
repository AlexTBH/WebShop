using WebShopBackend.Models;

namespace WebShopBackend.Interfaces
{
	public interface IUserService
	{
		public Task<WebshopUser> GetUserByEmail(string email);

	}
}
