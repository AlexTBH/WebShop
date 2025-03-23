using WebShopBackend.Models;
using WebShopBackend.Interfaces;
using System.Security.Claims;
using WebShopShared.Models;
using WebShopShared.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;


namespace WebShopBackend.Services.EndpointsServices
{
	public static class EndpointsMapper
	{
		public static void ProductEndpoints(this WebApplication app)
		{
			app.MapGet("/products", async (IProductService productService) =>
			{
				var products = await productService.GetProducts();
				var productDtos = products.Select(p => p.ProductToDto()).ToList();
				return Results.Ok(productDtos);
			});

			app.MapGet("/products/{id}", async (IProductService productService, int id) =>
			{
				var product = await productService.GetProduct(id);
				if (product == null)
				{
					return Results.NotFound($"Product with ID {id} not found");
				}
				return Results.Ok(product.ProductToDto());
			});
		}

		public static void UserEndpoints (this WebApplication app)
		{
			app.MapGroup("/Account").MapGet("/AuthenticatedUser", async Task<IResult> (ClaimsPrincipal user, WebShopDbContext context) =>
			{
				if (user?.Identity?.IsAuthenticated != true)
				{
					return TypedResults.Unauthorized();
				}

				string? userId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
				if (string.IsNullOrEmpty(userId))
				{
					return TypedResults.BadRequest("User ID claim is missing.");
				}

				var webShopUser = await context.Users.FindAsync(userId);
				if (webShopUser == null)
				{
					return TypedResults.NotFound("User not found");
				}

				var webShopUserDto = webShopUser.ToWebshopUserDto();
				return TypedResults.Ok(webShopUserDto);

			}).RequireAuthorization();
		}

		public static void OrderEndPoints (this WebApplication app)
		{
			app.MapPost("/add-to-cart", async (AddToCartDto addToCartDto, HttpContext httpContext, IOrderService orderService) =>
			{
				var userEmail = httpContext.User?.Identity?.Name;

				if (string.IsNullOrEmpty(userEmail))
				{
					return Results.Unauthorized();
				}

				await orderService.PostOrderProduct(addToCartDto, userEmail);

				return Results.Ok("Product added to order.");
			}).RequireAuthorization();

			app.MapGet("/GetOrders", async (HttpContext httpContext, IOrderService orderService) =>
			{
				var userEmail = httpContext.User?.Identity?.Name;

				if (string.IsNullOrEmpty(userEmail))
				{
					return Results.Unauthorized();
				}

				var fetchedProducts = await orderService.GetOrderProducts(userEmail);

				return Results.Ok(fetchedProducts);

			}).RequireAuthorization();

			app.MapPut("/changeOrderStatus/{id}", async (int id, IOrderService orderService) =>
			{
				await orderService.ChangeOrderStatus(id);
				return Results.Ok($"Order {id} status updated successfully.");
			});

			app.MapGet("/GetOrder/{email}", async (string email, WebShopDbContext _context) =>
			{
				var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

				if (user == null)
				{
					return Results.NotFound($"User with email {email} not found");
				}

				// Now that we have the UserId, fetch the order
				var order = await _context.Orders
										  .FirstOrDefaultAsync(o => o.UserId == user.Id && o.Status == OrderStatus.Pending);

				if (order == null)
				{
					return Results.NotFound($"Order not found for user {user.Email}");
				}

				return Results.Ok(order.Id); // Return the OrderId
			});
		}

		public static void CurrencyEndPoints(this WebApplication app)
		{
			app.MapPost("/SekToUsd", async (CurrencyDto request, ICurrencyExchange currencyExchange) =>
			{
				if (request.ConversionResult < 0)
				{
					return Results.BadRequest("Invalid SEK amount");
				}

				var convertedAmount = await currencyExchange.ConvertCurrency(
					  new CurrencyDto { ConversionResult = request.ConversionResult, TargetCurrency = request.TargetCurrency }
				);

				return Results.Ok(convertedAmount);
			});
		}
	}
}
