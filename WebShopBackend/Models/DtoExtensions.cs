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
				Description = product.Description,
				OnSale = product.OnSale,
				Price = product.Price,
				Url = product.Url,
				IsInStock = product.IsInStock
			};

			return productDto;
		}

		public static WebShopuserDto ToWebshopUserDto(this WebshopUser user)
		{
			
			WebShopuserDto userDto = new WebShopuserDto()
			{
				Username = user.UserName ?? string.Empty,
				Email = user.Email ?? string.Empty
			};

			return userDto;
		}
	}
}
