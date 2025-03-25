using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace WebShopFrontend.Services
{
	public class AuthorizationMessageHandler : DelegatingHandler
	{
		private readonly ILocalStorageService _localStorage;
		private readonly WebshopAuthenticationStateProvider _authState;

		public AuthorizationMessageHandler(ILocalStorageService localStorage, WebshopAuthenticationStateProvider authState)
		{
			_localStorage = localStorage;
			_authState = authState;
		}

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (_authState.IsInitialized)
			{
				var token = await _localStorage.GetItemAsStringAsync("jwtToken");

				if (!string.IsNullOrEmpty(token))
				{
					request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
				}
				else
				{
					Console.WriteLine("Token is missing!");
				}
			}
			else
			{
				Console.WriteLine("AuthState is not initialized!");
			}
			return await base.SendAsync(request, cancellationToken);
		}

	}
}
