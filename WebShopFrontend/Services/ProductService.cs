using WebShopShared.Interfaces;
using WebShopShared.Models;

namespace WebShopFrontend.Services
{
	public class ProductService : IProductService
	{
		private readonly HttpClient _httpClient;

		public ProductService(IHttpClientFactory httpClientFactory)
		{
			_httpClient = httpClientFactory.CreateClient("ProductApi");
		}
	
		public async Task<List<Product>> GetProducts()
		{
			return await _httpClient.GetFromJsonAsync<List<Product>>("products");
		}

		public async Task<Product> GetProduct(int id)
		{
			return await _httpClient.GetFromJsonAsync<Product>($"/{id}");
		}
	}
}
