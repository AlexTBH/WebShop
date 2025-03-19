using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using Microsoft.EntityFrameworkCore;
using WebShopBackend.Interfaces;
using WebShopBackend.Models;
using WebShopShared.Models;

namespace WebShopBackend.Services
{
	public class OrderService : IOrderService
	{

		private readonly WebShopDbContext _context;
		private readonly ILogger<OrderService> _logger;
		private readonly IUserService _userService;
		private readonly IProductService _productService;

		public OrderService(WebShopDbContext context, ILogger<OrderService> logger, IUserService userSerivce, IProductService productService)
		{
			_context = context;
			_logger = logger;
			_userService = userSerivce;
			_productService = productService;
		}
		public async Task PostOrderProduct(AddToCartDto addToCartDto, string userEmail)
		{
			var user = await _userService.GetUserByEmail(userEmail);
			var product = await _productService.GetProduct(addToCartDto.Id);

			var pendingOrder = await GetOrCreatePendingOrder(user);

			var existingOrderProduct = await _context.OrderProducts
				.FirstOrDefaultAsync(op => op.OrderId == pendingOrder.Id && op.ProductId == product.Id);

			if (existingOrderProduct != null)
			{
				if (product.Quantity >= addToCartDto.Quantity)
				{
					existingOrderProduct.Quantity += addToCartDto.Quantity;
					await _productService.ChangeQuantity(product.Id, addToCartDto.Quantity);
				}
			}
			else
			{
				if (product.Quantity >= addToCartDto.Quantity)
				{
					var orderProduct = new OrderProduct
					{
						ProductId = product.Id,
						OrderId = pendingOrder.Id,
						Quantity = addToCartDto.Quantity
					};

					_context.OrderProducts.Add(orderProduct);
					await _productService.ChangeQuantity(product.Id, addToCartDto.Quantity);
				}
			}

			await _context.SaveChangesAsync();
		}

		public async Task<List<OrderProduct>> GetOrderProducts(string userEmail)
		{
			var user = await _userService.GetUserByEmail(userEmail);

			var order = await _context.Orders
				.Include(o => o.OrderProducts)
				.FirstOrDefaultAsync(o => o.UserId == user.Id && o.Status == OrderStatus.Pending);

			if (order == null)
			{
				throw new KeyNotFoundException("No pending order found for this user.");
			}

			var products = order.OrderProducts.ToList();

			return order.OrderProducts.ToList();
		}

		public async Task<Order> GetOrCreatePendingOrder(WebshopUser user)
		{
			var pendingOrder = await _context.Orders
				.Where(o => o.UserId == user.Id && o.Status == OrderStatus.Pending)
				.FirstOrDefaultAsync();

			if (pendingOrder == null)
			{
				pendingOrder = new Order
				{
					UserId = user.Id,
					Status = OrderStatus.Pending
				};

				_context.Orders.Add(pendingOrder);
				await _context.SaveChangesAsync();
			}

			return pendingOrder;
		}
	} 
}
