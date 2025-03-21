﻿using WebShopBackend.Models;
using WebShopShared.Models;

namespace WebShopBackend.Interfaces
{
	public interface IOrderService
	{
		public Task PostOrderProduct(AddToCartDto productId, string userEmail);

		public Task<Order> GetOrCreatePendingOrder(WebshopUser user);

		public Task<List<OrderProduct>> GetOrderProducts(string userEmail);
	}
}
