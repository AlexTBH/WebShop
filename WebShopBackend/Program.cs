using Microsoft.AspNetCore.Hosting.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebShopBackend.Models;
using WebShopBackend.Services;
using WebShopBackend.Services.DatabaseServices;
using WebShopBackend.Services.EndpointsServices;
using WebShopBackend.Interfaces;



namespace WebShopBackend
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddScoped<IProductService, ProductService>();

			builder.Services.AddDbContext<WebShopDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("WebShopDb"));
				options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
			});

			var app = builder.Build();

			builder.Logging.AddConsole();

			using (var scope = app.Services.CreateScope())
			{
				var db = scope.ServiceProvider.GetRequiredService<WebShopDbContext>();
				db.Database.EnsureDeleted();
				db.Database.EnsureCreated();
				await DatabaseHelper.PopulateDatabase(db);
			}

			app.ProductEndpoints();

			app.Run();
		}
	}
}
