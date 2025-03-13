using Microsoft.AspNetCore.Components.Authorization;
using WebShopShared.Models;
using System.Security.Claims;
using System.Text.Json;


public class WebshopAuthenticationStateProvider : AuthenticationStateProvider
{
	private readonly IHttpClientFactory _httpClientFactory;

	public WebshopAuthenticationStateProvider(IHttpClientFactory httpClientFactory)
	{
		_httpClientFactory = httpClientFactory;
	}

	public override async Task<AuthenticationState> GetAuthenticationStateAsync()
	{
		var client = _httpClientFactory.CreateClient("WebShopApi");

		try
		{
			var response = await client.GetAsync("/Account/AuthenticatedUser");

			if (response.IsSuccessStatusCode)
			{
				var json = await response.Content.ReadAsStringAsync();
				var user = JsonSerializer.Deserialize<WebShopuserDto>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

				if (user != null && !string.IsNullOrEmpty(user.Username))
				{
					var claims = new List<Claim>
					{
						new Claim(ClaimTypes.Name, user.Username),
						new Claim(ClaimTypes.Email, user.Email ?? string.Empty)
					};

					var identity = new ClaimsIdentity(claims, "WebshopAuth");
					var userPrincipal = new ClaimsPrincipal(identity);

					return new AuthenticationState(userPrincipal);
				}
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error fetching user: {ex.Message}");
		}

		return new AuthenticationState(new ClaimsPrincipal()); 
	}

	public void NotifyUserAuthentication(string username, string email)
	{
		var claims = new List<Claim>
		{
			new Claim(ClaimTypes.Name, username),
			new Claim(ClaimTypes.Email, email)
		};

		var identity = new ClaimsIdentity(claims, "WebshopAuth");
		var userPrincipal = new ClaimsPrincipal(identity);

		NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(userPrincipal)));
	}

	public void NotifyUserLogout()
	{
		var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
		NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymousUser)));
	}
}
