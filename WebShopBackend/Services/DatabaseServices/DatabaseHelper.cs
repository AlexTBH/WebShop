using WebShopBackend.Models;


namespace WebShopBackend.Services.DatabaseServices
{
	static public class DatabaseHelper
	{
		public static async Task PopulateDatabase(WebShopDbContext db)
		{
			var products = ProductData.GetSampleProducts();

			foreach(var product in products)
			{
				db.Products.Add(product);
			}
			await db.SaveChangesAsync();
		}
	}
}
