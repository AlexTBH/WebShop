using WebShopFrontend.Interfaces;
using WebShopShared.Models;

namespace WebShopFrontend.Services
{
	public class OrderService : IOrderService
	{
		private readonly HttpClient _httpClient;
		private readonly ILogger<OrderService> _logger;
		private readonly IProductService _productService;

		public OrderService(IHttpClientFactory httpClientFactory, ILogger<OrderService> logger, IProductService productService)
		{
			_httpClient = httpClientFactory.CreateClient("WebShopApi"); 
			_logger = logger;
			_productService = productService;
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

		public async Task<List<ProductDto>> GetOrderProducts()
		{
			var response = await _httpClient.GetFromJsonAsync<List<OrderProductDto>>("GetOrders");



			List<Task<ProductDto>> productTasks = new List<Task<ProductDto>>();
			
			foreach (OrderProductDto order in response)
			{
				productTasks.Add(_productService.GetProduct(order.ProductId));
			}

			var products = await Task.WhenAll(productTasks);

			return products.ToList();
		}
	}
}
