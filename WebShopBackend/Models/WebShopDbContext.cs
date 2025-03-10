﻿using Microsoft.AspNetCore.Identity;
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
	}

	public class WebshopUser : IdentityUser
	{

	}
}
