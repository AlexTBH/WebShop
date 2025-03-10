using System.Text.Json;
using WebShopFrontend.Interfaces;
using WebShopFrontend.Models;
using System.Text;

namespace WebShopFrontend.Services
{
	public class UserService : IUserService
	{
		private readonly HttpClient _httpClient;
		private readonly ILogger<UserService> _logger;

		public UserService(IHttpClientFactory httpClientFactory, ILogger<UserService> logger)
		{
			_httpClient = httpClientFactory.CreateClient("WebShopApi");
			_logger = logger;
		}

		public async Task RegisterUser(UserDto user)
		{
			try
			{
				var json = JsonSerializer.Serialize<UserDto>(user);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				var response = await _httpClient.PostAsync("/Account/register", content);

				if(!response.IsSuccessStatusCode)
				{
					var errorMessage = await response.Content.ReadAsStringAsync();
					throw new HttpRequestException($"Registrering misslyckades: {errorMessage}");
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Fel vid registrering");
				throw;
			}
		}
	}
}
