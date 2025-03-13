using System.Text.Json;
using WebShopFrontend.Interfaces;
using WebShopFrontend.Models;
using System.Text;
using System.Security.Claims;

namespace WebShopFrontend.Services
{
	public class UserService : IUserService
	{
		private readonly HttpClient _httpClient;
		private readonly ILogger<UserService> _logger;
		private readonly WebshopAuthenticationStateProvider _authState;

		public UserService(IHttpClientFactory httpClientFactory, ILogger<UserService> logger, WebshopAuthenticationStateProvider authState)
		{
			_httpClient = httpClientFactory.CreateClient("WebShopApi");
			_logger = logger;
			_authState = authState;
		}
		public async Task<bool> RegisterUser(RegisterDto user)
		{
			try
			{
				var content = UserToHttpContent(user);
				var response = await _httpClient.PostAsync("/Account/register", content);

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
				var content = UserToHttpContent(user);
				var response = await _httpClient.PostAsync("/Account/login?useCookies=true", content);
			
				if (!response.IsSuccessStatusCode)
				{
					_logger.LogError("Failed inlog");
					return false;
				}

				var authState = await _authState.GetAuthenticationStateAsync();
				var userPrincipal = authState.User;
			
				if (userPrincipal.Identity?.IsAuthenticated == true)
				{
					var username = userPrincipal.Identity.Name ?? string.Empty;
					var email = userPrincipal.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;

					_authState.NotifyUserAuthentication(username, email);
				}

				return true;
			}
			catch (Exception ex) 
			{
				_logger.LogError(ex, "Misslyckad inloggning");
				return false;
			};
		}

		private static StringContent UserToHttpContent<T>(T user)
		{
			var json = JsonSerializer.Serialize(user);
			return new StringContent(json, Encoding.UTF8, "application/json");
		}

	}
}
