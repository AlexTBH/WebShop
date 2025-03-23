using WebShopFrontend.Interfaces;
using WebShopShared.Models;

namespace WebShopFrontend.Services
{
	public class OrderService : IOrderService
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly ILogger<OrderService> _logger;

		public OrderService(IHttpClientFactory httpClientFactory, ILogger<OrderService> logger)
		{
			_httpClientFactory = httpClientFactory;  
			_logger = logger;
		}

		public async Task AddToCart(AddToCartDto addtoCartDto)
		{
			var client = _httpClientFactory.CreateClient("WebShopApi"); 
			var response = await client.PostAsJsonAsync("/add-to-cart", addtoCartDto);

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

		public async Task<List<OrderProductDetailsDto>> GetOrderProducts()
		{
			var client = _httpClientFactory.CreateClient("WebShopApi");
			var response = await client.GetFromJsonAsync<List<OrderProductDetailsDto>>("GetOrders");

			if (response == null)
			{
				throw new HttpRequestException("Failed to retrieve orders.");
			}

			return response;
		}
	}
}
