using System.Net.Http;
using WebShopFrontend.Interfaces;
using WebShopShared.Models;
using System.Net.Http.Headers;
using Blazored.LocalStorage;

namespace WebShopFrontend.Services
{
	public class OrderService : IOrderService
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly ILogger<OrderService> _logger;
		private readonly IUserService _userService;
		private readonly ILocalStorageService _localStorage;

		public OrderService(
			IHttpClientFactory httpClientFactory,
			ILogger<OrderService> logger,
			IUserService userService,
			ILocalStorageService localStorage)
		{
			_httpClientFactory = httpClientFactory;
			_logger = logger;
			_userService = userService;
			_localStorage = localStorage;
		}

		public async Task AddToCart(AddToCartDto addtoCartDto)
		{
			var client = _httpClientFactory.CreateClient("WebShopApiAuth");

			var token = await _localStorage.GetItemAsStringAsync("jwtToken");


			if (!string.IsNullOrEmpty(token))
			{
				client.DefaultRequestHeaders.Authorization =
					new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
			}

			var response = await client.PostAsJsonAsync("/add-to-cart", addtoCartDto);

			if (!response.IsSuccessStatusCode)
			{
				var errorMessage = await response.Content.ReadAsStringAsync();
				throw new Exception($"An error occurred while adding the product to the cart. " + $"Status code: {response.StatusCode}, Response: {errorMessage}");
			}
		}

		public async Task<int> GetOrderId()
		{
			var user = await _userService.GetUser();
			var token = await _localStorage.GetItemAsStringAsync("jwtToken");
			try
			{
				var client = _httpClientFactory.CreateClient("WebShopApiAuth");
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
			var client = _httpClientFactory.CreateClient("WebShopApiAuth");
			var response = await client.PutAsync($"/changeOrderStatus/{id}", null);

			if (!response.IsSuccessStatusCode)
			{
				throw new Exception($"Failed to update order status. Status Code: {response.StatusCode}");
			}
		}

		public async Task<List<OrderProductDetailsDto>> GetOrderProducts()
		{
			List<OrderProductDetailsDto> orderList = new();
			{
				try
				{
					var token = await _localStorage.GetItemAsStringAsync("jwtToken");

					var client = _httpClientFactory.CreateClient("WebShopApiAuth");

					if (!string.IsNullOrEmpty(token))
					{
						client.DefaultRequestHeaders.Authorization =
							new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
					}

					var response = await client.GetFromJsonAsync<List<OrderProductDetailsDto>>("GetOrders");

					if (response != null)
					{
						orderList = response;
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Failed to fetch orders: {ex.Message}");
				}

				return orderList;
			}
		}
	}
}
