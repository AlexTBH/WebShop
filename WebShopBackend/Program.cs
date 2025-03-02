using Microsoft.AspNetCore.Hosting.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebShopBackend.Model;
using WebShopBackend.Services;

namespace WebShopBackend
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			

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
				DatabaseHelper.PopulateDatabase(db);
			}

			//app.MapGet("/", () => "Hello World!");

			app.Run();
		}
	}
}
