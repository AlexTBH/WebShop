using WebShopFrontend.Interfaces;
using WebShopShared.Models;

namespace WebShopFrontend.Services
{
	public class ProductService : IProductService
	{
		private readonly HttpClient _httpClient;
		private readonly ILogger<ProductService> _logger;

		public ProductService(IHttpClientFactory httpClientFactory, ILogger<ProductService> logger)
		{
			_httpClient = httpClientFactory.CreateClient("WebShopApi");
			_logger = logger;
		}
	
		public async Task<List<ProductDto>> GetProducts()
		{
			try
			{
				var products = await _httpClient.GetFromJsonAsync<List<ProductDto>>("products");

				if(products == null)
				{
					_logger.LogWarning("No products could not be fetched from the API");
					return new List<ProductDto>();
				}

				return products;
			}
			catch (Exception ex)
			{
				_logger.LogWarning(ex, "Could not fetch products from the api");
				return new List<ProductDto>();
			}
		}

		public async Task<ProductDto> GetProduct(int id)
		{
			try
			{
				var product = await _httpClient.GetFromJsonAsync<ProductDto>($"products/{id}");

				if (product == null)
				{
					_logger.LogWarning("The product could not be fetched from the API");
					throw new InvalidOperationException("Product not found");
				}
				return product;
			}
			catch (Exception ex)
			{
				_logger.LogWarning($"Could not fetch the product from API {ex.Message}");
				throw;
			}
		}
	}
}
