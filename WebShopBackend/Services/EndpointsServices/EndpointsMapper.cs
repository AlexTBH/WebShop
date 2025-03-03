using WebShopBackend.Model;
using WebShopShared.Interfaces;

namespace WebShopBackend.Services.EndpointsServices
{
	public static class EndpointsMapper
	{
		public static void ProductEndpoints(this WebApplication app)
		{
			app.MapGet("/products", async (IProductService productService) => await productService.GetProducts());
			app.MapGet("/products/{id}", async (IProductService productService, int id) => await productService.GetProduct(id));
		}
	}
}
