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
		public async Task PostOrderProduct(AddToCartDto productId, string userEmail)
		{
			var user = await _userService.GetUserByEmail(userEmail);
			var product = await _productService.GetProduct(productId.Id);

			var pendingOrder = await GetOrCreatePendingOrder(user);

			var orderProduct = new OrderProduct
			{
				ProductId = product.Id,
				OrderId = pendingOrder.Id
			};

			pendingOrder.OrderProducts.Add(orderProduct);
			await _context.SaveChangesAsync();

		}


		private async Task<Order> GetOrCreatePendingOrder(WebshopUser user)
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
			}

			return pendingOrder;
		}
	} 
}
