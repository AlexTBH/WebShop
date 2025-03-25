using WebShopFrontend.Models;

namespace WebShopFrontend.Interfaces
{
	public interface IUserService
	{
		public Task<bool> RegisterUser(RegisterDto user);
		public Task<bool> UserLogin(LoginDto user);
		public Task<string> GetUser();

	}
}
