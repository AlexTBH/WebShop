using WebShopBackend.Services.Configurations;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Text.Json;
using WebShopShared.Models;
using WebShopShared.Interfaces; 

namespace WebShopBackend.Services
{
	public class NinjaApiService : ICurrencyExchange
	{
		private readonly HttpClient _httpClient;
		private readonly string _apiKey;

		public NinjaApiService(HttpClient httpClient, IOptions<NinjaApiSettings> options)
		{
			_httpClient = httpClient;
			_apiKey = options.Value.ApiKey;
		}

		public async Task<CurrencyDto> ConvertCurrency(CurrencyDto request)
		{
			var amount = request.ConversionResult.ToString(CultureInfo.InvariantCulture);
			var requestUrl = $"https://v6.exchangerate-api.com/v6/{_apiKey}/pair/SEK/{request.TargetCurrency}/{amount}";

			var response = await _httpClient.GetAsync(requestUrl);

			if (!response.IsSuccessStatusCode)
			{
				Console.WriteLine($"Error: API request failed with status code: {response.StatusCode}");
				return new CurrencyDto { ConversionResult = 0, TargetCurrency = request.TargetCurrency };
			}

			var json = await response.Content.ReadAsStringAsync();
			var result = JsonSerializer.Deserialize<JsonElement>(json, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			});

			double conversionResult = 0;
			if (result.TryGetProperty("conversion_result", out var conversionResultProp))
			{
				conversionResult = conversionResultProp.GetDouble();
			}

			return new CurrencyDto
			{
				ConversionResult = conversionResult,
				TargetCurrency = request.TargetCurrency
			};
		}
	}
}
