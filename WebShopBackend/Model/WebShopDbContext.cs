using Microsoft.EntityFrameworkCore;
using WebShopShared.Models;

namespace WebShopBackend.Model
{
	public class WebShopDbContext : DbContext
	{
		public WebShopDbContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Product> Products { get; set; }
	}
}
