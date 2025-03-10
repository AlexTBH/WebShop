using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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

			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddAuthorization();
			builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
				.AddCookie(IdentityConstants.ApplicationScheme);

			builder.Services.AddIdentityCore<WebshopUser>()
				.AddEntityFrameworkStores<WebShopDbContext>()
				.AddApiEndpoints();

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

			app.MapGroup("/Account").MapIdentityApi<WebshopUser>();

			app.Run();
		}
	}
}
