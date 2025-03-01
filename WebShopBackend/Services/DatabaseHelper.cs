using WebShopBackend.Model;
using WebShopShared;

namespace WebShopBackend.Services
{
	static public class DatabaseHelper
	{
		public static void PopulateDatabase(WebShopDbContext db)
		{
			var products = ProductData.GetSampleProducts();

			foreach(var product in products)
			{
				db.Add(product);
			}
			db.SaveChangesAsync();
		}
	}
}
