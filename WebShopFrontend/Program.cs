using WebShopFrontend.Components;
using WebShopFrontend.Services;
using System.Net;
using WebShopFrontend.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using WebShopShared.Interfaces;
using WebShopFrontend.Models;
using Blazored.LocalStorage;


namespace WebShopFrontend;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

		builder.Services.AddHttpClient("WebShopApiAuth", client =>
		{
			client.BaseAddress = new Uri("https://localhost:7011/");
		})
		.AddHttpMessageHandler(sp =>
		{
			var localStorage = sp.GetRequiredService<ILocalStorageService>();
			var authState = sp.GetRequiredService<WebshopAuthenticationStateProvider>();
			return new AuthorizationMessageHandler(localStorage, authState);
		});

		builder.Services.AddHttpClient("WebShopApi", client =>
		{
			client.BaseAddress = new Uri("https://localhost:7011/");
		});

		builder.Services.AddCascadingAuthenticationState();



		builder.Services.AddLogging();
		builder.Services.AddBlazoredLocalStorage();

		builder.Services.AddAuthorization();
        builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
            .AddCookie(IdentityConstants.ApplicationScheme);

        
        builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
            sp.GetRequiredService<WebshopAuthenticationStateProvider>());

		builder.Services.AddScoped<IProductService, ProductService>();
		builder.Services.AddScoped<IUserService, UserService>();
		builder.Services.AddScoped<IOrderService, OrderService>();
        builder.Services.AddScoped<ICurrencyExchange, ExchangeService>();
		builder.Services.AddScoped<OrderStateService>();

		builder.Services.AddScoped<WebshopAuthenticationStateProvider>();


		var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseAntiforgery();


        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}
