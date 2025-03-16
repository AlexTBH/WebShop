using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebShopShared.Models;

namespace WebShopBackend.Models
{
	public class WebShopDbContext : IdentityDbContext<WebshopUser>
	{
		public WebShopDbContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Product> Products { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderProduct> OrderProducts {get; set;}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<OrderProduct>()
				.HasKey(op => new { op.OrderId, op.ProductId });

			modelBuilder.Entity<OrderProduct>()
				.HasOne(op => op.Order) 
				.WithMany(o => o.OrderProducts) 
				.HasForeignKey(op => op.OrderId); 

			modelBuilder.Entity<OrderProduct>()
				.HasOne(op => op.Product) // 
				.WithMany(p => p.OrderProducts) 
				.HasForeignKey(op => op.ProductId); 
		}
	}

	public class WebshopUser : IdentityUser
	{

	}
}
