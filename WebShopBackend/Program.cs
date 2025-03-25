using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebShopBackend.Models;
using WebShopBackend.Services;
using WebShopBackend.Services.DatabaseServices;
using WebShopBackend.Services.EndpointsServices;
using WebShopBackend.Interfaces;
using WebShopBackend.Services.Configurations;
using WebShopShared.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebShopBackend
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var jwtKey = builder.Configuration["Jwt:Secret"];
			var jwtIssuer = builder.Configuration["Jwt:Issuer"];

			if (string.IsNullOrEmpty(jwtKey))
			{
				throw new Exception("JWT Key is missing or empty!");
			}


			builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidateAudience = false,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = jwtIssuer,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
					};
				});

			builder.Services.AddAuthorization();

			builder.Services.AddIdentityCore<WebshopUser>()
				.AddEntityFrameworkStores<WebShopDbContext>()
				.AddApiEndpoints();

			builder.Services.AddScoped<JwtService>();
			builder.Services.AddScoped<IProductService, ProductService>();
			builder.Services.AddScoped<IOrderService, OrderService>();
			builder.Services.AddScoped<IUserService, UserService>();
			builder.Services.AddHttpClient<ICurrencyExchange, NinjaApiService>();

			builder.Services.AddDbContext<WebShopDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("WebShopDb"));
				options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
			});

			builder.Services.Configure<NinjaApiSettings>(builder.Configuration.GetSection("NinjaApiSettings"));


			var app = builder.Build();

			builder.Logging.AddConsole();

			using (var scope = app.Services.CreateScope())
			{
				var db = scope.ServiceProvider.GetRequiredService<WebShopDbContext>();
				db.Database.EnsureDeleted();
				db.Database.EnsureCreated();
				await DatabaseHelper.PopulateDatabase(db);
			}

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.ProductEndpoints();
			app.UserEndpoints();
			app.OrderEndPoints();
			app.CurrencyEndPoints();
			app.MapGroup("/Account").MapIdentityApi<WebshopUser>();


			app.Run();


		}
	}
}
