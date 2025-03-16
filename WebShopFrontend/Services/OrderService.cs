using WebShopFrontend.Interfaces;
using WebShopShared.Models;

namespace WebShopFrontend.Services
{
	public class OrderService : IOrderService
	{
		private readonly HttpClient _httpClient;
		private readonly ILogger<OrderService> _logger;

		public OrderService(IHttpClientFactory httpClientFactory, ILogger<OrderService> logger)
		{
			_httpClient = httpClientFactory.CreateClient("WebShopApi"); 
			_logger = logger;
		}

		public async Task AddToCart(AddToCartDto productId)
		{
			var response = await _httpClient.PostAsJsonAsync("/add-to-cart", productId);
		
			if (response.IsSuccessStatusCode)
			{
				var message = await response.Content.ReadAsStringAsync();
				_logger.LogInformation("Product added to cart: " + message);
			}
			else
			{
				_logger.LogError("Failed to add product to cart.");
				Console.WriteLine(response);
			}
		}
	}
}
