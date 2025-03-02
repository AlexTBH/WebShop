using WebShopShared;
namespace WebShopBackend.Model
{
	public interface IProductService
	{
		public Task<List<Product>> GetProducts();
		public Task<Product> GetProduct(int id);

	}
}
