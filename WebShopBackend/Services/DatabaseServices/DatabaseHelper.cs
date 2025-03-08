using WebShopBackend.Models;


namespace WebShopBackend.Services.DatabaseServices
{
	static public class DatabaseHelper
	{
		public static void PopulateDatabase(WebShopDbContext db)
		{
			var products = ProductData.GetSampleProducts();

			foreach(var product in products)
			{
				db.Products.Add(product);
			}
			db.SaveChangesAsync();
		}
	}
}
