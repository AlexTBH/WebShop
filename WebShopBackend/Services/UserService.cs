using Microsoft.EntityFrameworkCore;
using WebShopBackend.Interfaces;
using WebShopBackend.Models;

namespace WebShopBackend.Services
{
	public class UserService : IUserService
	{
		private readonly WebShopDbContext _context;

		public UserService(WebShopDbContext context)
		{
			_context = context;
		}

		public async Task<WebshopUser> GetUserByEmail(string email)
		{
			var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
			if (user == null)
			{
				throw new Exception("User not found.");
			}
			return user;
		}
	}
}
