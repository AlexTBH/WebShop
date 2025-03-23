using System.Net.Http;
using WebShopFrontend.Interfaces;
using WebShopShared.Models;

namespace WebShopFrontend.Services
{
	public class OrderService : IOrderService
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly ILogger<OrderService> _logger;
		private readonly IUserService _userService;

		public OrderService(IHttpClientFactory httpClientFactory, ILogger<OrderService> logger, IUserService userService)
		{
			_httpClientFactory = httpClientFactory;  
			_logger = logger;
			_userService = userService;
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

		public async Task<int> GetOrderId()
		{
			var user = await _userService.GetUser(); // Ensure GetUser is asynchronous if needed.

			try
			{
				var client = _httpClientFactory.CreateClient("WebShopApi");
				var response = await client.GetAsync($"/GetOrder/{user}"); // Use await for asynchronous call

				if (response.IsSuccessStatusCode)
				{
					// Read the response content (OrderId in this case) as a string
					var orderId = await response.Content.ReadAsStringAsync();

					// Convert the orderId to an integer
					if (int.TryParse(orderId, out var parsedOrderId))
					{
						return parsedOrderId;
					}
					else
					{
						// Return a default or error value if conversion fails
						return -1; // or handle as per your error management strategy
					}
				}
				else
				{
					// Return -1 or some other value to indicate failure
					return -1;
				}
			}
			catch (Exception ex)
			{
				// Log or handle exceptions
				Console.WriteLine($"Error fetching orderId: {ex.Message}");
				return -1;
			}

		}

		public async Task ChangeOrderStatus(int id)
		{
			var client = _httpClientFactory.CreateClient("WebShopApi");

			var response = await client.PutAsync($"/changeOrderStatus/{id}", null);

			if (!response.IsSuccessStatusCode)
			{
				throw new Exception($"Failed to update order status. Status Code: {response.StatusCode}");
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
