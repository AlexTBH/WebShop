using WebShopBackend.Models;
using WebShopBackend.Interfaces;




namespace WebShopBackend.Services.EndpointsServices
{
	public static class EndpointsMapper
	{
		public static void ProductEndpoints(this WebApplication app)
		{
			app.MapGet("/products", async (IProductService productService) =>
			{
				var products = await productService.GetProducts();
				var productDtos = products.Select(p => p.ProductToDto()).ToList();
				return Results.Ok(productDtos);
			});

			app.MapGet("/products/{id}", async (IProductService productService, int id) =>
			{
				var product = await productService.GetProduct(id);
				if (product == null)
				{
					return Results.NotFound($"Product with ID {id} not found");
				}
				return Results.Ok(product.ProductToDto());
			});
		}
	}
}
