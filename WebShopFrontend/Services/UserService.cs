using System.Text.Json;
using WebShopFrontend.Interfaces;
using WebShopFrontend.Models;
using System.Text;
using System.Security.Claims;

namespace WebShopFrontend.Services
{
	public class UserService : IUserService
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly ILogger<UserService> _logger;
		private readonly WebshopAuthenticationStateProvider _authState;

		public UserService(IHttpClientFactory httpClientFactory, ILogger<UserService> logger, WebshopAuthenticationStateProvider authState)
		{
			_httpClientFactory = httpClientFactory;
			_logger = logger;
			_authState = authState;
		}

		public async Task<bool> RegisterUser(RegisterDto user)
		{
			try
			{
				var client = _httpClientFactory.CreateClient("WebShopApi"); 
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
				var client = _httpClientFactory.CreateClient("WebShopApi"); 
				var content = UserToHttpContent(user);
				var response = await client.PostAsync("/Account/login?useCookies=true", content);

				if (!response.IsSuccessStatusCode)
				{
					_logger.LogError("Failed login");
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
