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
		public Task<List<ProductDto>> GetProducts();
		public Task<ProductDto> GetProduct(int id);
	}
}
