using WebShopFrontend.Models;

namespace WebShopFrontend.Interfaces
{
	public interface IUserService
	{
		public Task RegisterUser(UserDto user);
	}
}
