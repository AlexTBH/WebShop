using Microsoft.JSInterop;
using System.Threading.Tasks;

public class LocalStorageService
{
	private readonly IJSRuntime _jsRuntime;

	public LocalStorageService(IJSRuntime jsRuntime)
	{
		_jsRuntime = jsRuntime;
	}

	// This method is used to check if the JavaScript interop call can happen safely
	public async Task<string> GetItemAsync(string key)
	{
		// Ensure JavaScript interop is only called once the component is rendered on the client-side
		if (IsRunningOnServer())
		{
			return null; // Avoid JS interop calls on the server side
		}

		// Only do JS interop once we are sure the client-side rendering is done
		return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);
	}

	public async Task SetItemAsync(string key, string value)
	{
		if (IsRunningOnServer())
		{
			return; // Avoid JS interop calls on the server side
		}

		await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, value);
	}

	public async Task RemoveItemAsync(string key)
	{
		if (IsRunningOnServer())
		{
			return; // Avoid JS interop calls on the server side
		}

		await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
	}

	private bool IsRunningOnServer()
	{
		// In Blazor Server, there is no `window` or `document`, so we can check if we are on the server
		return _jsRuntime is not IJSInProcessRuntime;
	}
}
