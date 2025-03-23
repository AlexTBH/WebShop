using WebShopFrontend.Interfaces;
using WebShopShared.Models;

namespace WebShopFrontend.Services
{
	public class ProductService : IProductService
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly ILogger<ProductService> _logger;

		public ProductService(IHttpClientFactory httpClientFactory, ILogger<ProductService> logger)
		{
			_httpClientFactory = httpClientFactory;
			_logger = logger;
		}

		public async Task<List<ProductDto>> GetProducts()
		{
			try
			{
				var client = _httpClientFactory.CreateClient("WebShopApi"); 
				var products = await client.GetFromJsonAsync<List<ProductDto>>("products");

				if (products == null)
				{
					_logger.LogWarning("No products could not be fetched from the API");
					return new List<ProductDto>();
				}

				return products;
			}
			catch (Exception ex)
			{
				_logger.LogWarning(ex, "Could not fetch products from the API");
				return new List<ProductDto>();
			}
		}

		public async Task<ProductDto> GetProduct(int id)
		{
			try
			{
				var client = _httpClientFactory.CreateClient("WebShopApi"); 
				var product = await client.GetFromJsonAsync<ProductDto>($"products/{id}");

				if (product == null)
				{
					_logger.LogWarning("The product could not be fetched from the API");
					throw new InvalidOperationException("Product not found");
				}

				return product;
			}
			catch (Exception ex)
			{
				_logger.LogWarning($"Could not fetch the product from API: {ex.Message}");
				throw;
			}
		}
	}
}
