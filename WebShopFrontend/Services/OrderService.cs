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

			if (!response.IsSuccessStatusCode)
			{
				throw new Exception("An error occurred while adding the product to the cart.");
			}

		}

		public async Task<int> GetOrderId()
		{
			var user = await _userService.GetUser(); 

			try
			{
				var client = _httpClientFactory.CreateClient("WebShopApi");
				var response = await client.GetAsync($"/GetOrder/{user}"); 

				if (response.IsSuccessStatusCode)
				{
					
					var orderId = await response.Content.ReadAsStringAsync();

					if (int.TryParse(orderId, out var parsedOrderId))
					{
						return parsedOrderId;
					}
					else
					{
						return -1;
					}
				}
				else
				{
					return -1;
				}
			}
			catch (Exception ex)
			{
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
