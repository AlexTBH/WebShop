using WebShopShared;
using WebShopShared.Models;


namespace WebShopBackend.Models
{
	public static class DtoExtensions
	{
		public static ProductDto ProductToDto(this Product product)
		{
			ProductDto productDto = new ProductDto()
			{
				Id = product.Id,
				Name = product.Name,
				Price = product.Price,
				Url = product.Url,
				IsInStock = product.IsInStock
			};

			return productDto;
		}
	}
}
