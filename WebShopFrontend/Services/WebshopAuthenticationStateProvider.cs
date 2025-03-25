using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.WebUtilities;

namespace WebShopFrontend.Services
{
	public class WebshopAuthenticationStateProvider : AuthenticationStateProvider
	{
		private readonly ILocalStorageService _localStorage;
		private string? _jwtToken;
		private bool _isInitialized;

		public WebshopAuthenticationStateProvider(ILocalStorageService localStorage)
		{
			_localStorage = localStorage;
			_isInitialized = false;
		}

		public bool IsInitialized => _isInitialized;

		public async Task SetJwtTokenFromLocalStorage()
		{
			_jwtToken = await _localStorage.GetItemAsStringAsync("jwtToken");
			_isInitialized = true;
		}

		public override Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			var identity = new ClaimsIdentity();

			if (!string.IsNullOrEmpty(_jwtToken))
			{
				identity = new ClaimsIdentity(ParseClaimsFromJwt(_jwtToken), "jwt");
			}

			var user = new ClaimsPrincipal(identity);


			return Task.FromResult(new AuthenticationState(user));
		}

		public void NotifyUserAuthentication(string token)
		{
			_jwtToken = token;
			_isInitialized = true; 
			NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
		}

		public void NotifyUserLogout()
		{
			_jwtToken = null;
			_isInitialized = false; 
			NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
		}


		private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
		{
			var parts = jwt.Split('.');
			if (parts.Length != 3)
			{
				throw new ArgumentException("JWT must have three parts.");
			}

			var payload = parts[1];
			var jsonBytes = WebEncoders.Base64UrlDecode(payload);
			var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
			
			if (keyValuePairs == null)
			{
				throw new ArgumentException("JWT payload is not valid or is empty.");
			}

			return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value?.ToString() ?? string.Empty));
		}
	}
}