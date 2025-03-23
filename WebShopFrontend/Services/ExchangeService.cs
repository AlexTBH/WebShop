using WebShopShared.Interfaces;
using WebShopShared.Models;
using System.Text.Json;
using System.Text;

namespace WebShopFrontend.Services
{
	public class ExchangeService : ICurrencyExchange
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public ExchangeService(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}
		public async Task<CurrencyDto> ConvertCurrency(CurrencyDto request)
		{
			try
			{
				var client = _httpClientFactory.CreateClient("WebShopApi");
				var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

				var response = await client.PostAsync("/SekToUsd", jsonContent);

				if (!response.IsSuccessStatusCode)
				{
					Console.WriteLine($"API call failed: {response.StatusCode}");
					return new CurrencyDto { ConversionResult = 0, TargetCurrency = request.TargetCurrency };
				}

				var json = await response.Content.ReadAsStringAsync();

				var currencyDto = JsonSerializer.Deserialize<CurrencyDto>(json, new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				});

				if (currencyDto == null || currencyDto.ConversionResult == 0)
				{
					Console.WriteLine("Error: Conversion failed.");
					return new CurrencyDto { ConversionResult = 0, TargetCurrency = request.TargetCurrency };
				}

				return currencyDto;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
				return new CurrencyDto { ConversionResult = 0, TargetCurrency = request.TargetCurrency };
			}
		}
	}
}
