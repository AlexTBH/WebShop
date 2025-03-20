using WebShopBackend.Services.Configurations;
using Microsoft.Extensions.Options;
using System.Text.Json;
using WebShopBackend.Models;
using WebShopBackend.Interfaces;

namespace WebShopBackend.Services
{
	public class NinjaApiService : ICurrencyExchangeApi
	{
		private readonly HttpClient _httpClient;
		private readonly string _apiKey;

		public NinjaApiService(HttpClient httpClient, IOptions<NinjaApiSettings> options)
		{
			_httpClient = httpClient;
			_apiKey = options.Value.ApiKey;
		}

		public async Task<decimal> GetUSD(decimal sek)
		{
			var requestUrl = $"https://v6.exchangerate-api.com/v6/{_apiKey}/pair/SEK/USD/{sek}";

			var response = await _httpClient.GetAsync(requestUrl);

			var json = await response.Content.ReadAsStringAsync();

			Console.WriteLine("API Response: " + json);

			if (!response.IsSuccessStatusCode)
			{
				Console.WriteLine("Error: API request failed with status code: " + response.StatusCode);
				return 0;
			}

			var result = JsonSerializer.Deserialize<CurrencyResponse>(json, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			});

			return result?.ConversionResult ?? 0;
		}
	}
}
