using System.Text.Json;
using WebShopFrontend.Interfaces;
using WebShopFrontend.Models;
using System.Text;
using System.Security.Claims;
using Blazored.LocalStorage;

namespace WebShopFrontend.Services
{
	public class UserService : IUserService
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly ILogger<UserService> _logger;
		private readonly WebshopAuthenticationStateProvider _authState;
		private readonly ILocalStorageService _localStorage;

		public UserService(IHttpClientFactory httpClientFactory, ILogger<UserService> logger, WebshopAuthenticationStateProvider authState, ILocalStorageService localStorage)
		{
			_httpClientFactory = httpClientFactory;
			_logger = logger;
			_authState = authState;
			_localStorage = localStorage;
		}

		public async Task<bool> RegisterUser(RegisterDto user)
		{
			try
			{
				var client = _httpClientFactory.CreateClient("WebShopApiAuth"); 
				var content = UserToHttpContent(user);
				var response = await client.PostAsync("/Account/register", content);

				if (!response.IsSuccessStatusCode)
				{
					var errorMessage = await response.Content.ReadAsStringAsync();
					_logger.LogError($"Registrerings misslyckades: {errorMessage}");
					return false;
				}
				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Fel vid registrering");
				return false;
			}
		}

		public async Task<bool> UserLogin(LoginDto user)
		{
			try
			{
				var client = _httpClientFactory.CreateClient("WebShopApiAuth");
				var content = UserToHttpContent(user);
				var response = await client.PostAsync("/login", content);

				if (!response.IsSuccessStatusCode)
				{
					return false;
				}

				var responseContent = await response.Content.ReadAsStringAsync();
				Console.WriteLine($"API Response: {responseContent}");
				var tokenObj = JsonSerializer.Deserialize<JsonElement>(responseContent);
				var token = tokenObj.GetProperty("token").GetString();

				if (!string.IsNullOrEmpty(token))
				{
					await _localStorage.SetItemAsStringAsync("jwtToken", token);
					_authState.NotifyUserAuthentication(token);
				}

				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Failed login");
				return false;
			}
		}

		public async Task<string> GetUser()
		{
			var authenticationState = await _authState.GetAuthenticationStateAsync();

			var user = authenticationState.User;

			var username = user.Identity?.Name ?? "Guest";
			var email = user.FindFirst(ClaimTypes.Email)?.Value ?? "No Email";

			return username;
		}

		private static StringContent UserToHttpContent<T>(T user)
		{
			var json = JsonSerializer.Serialize(user);
			return new StringContent(json, Encoding.UTF8, "application/json");
		}
	}
}
