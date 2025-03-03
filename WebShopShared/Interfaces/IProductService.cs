using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopShared.Models;

namespace WebShopShared.Interfaces
{
	public interface IProductService
	{
		public Task<List<Product>> GetProducts();
		public Task<Product> GetProduct(int id);
	}
}
