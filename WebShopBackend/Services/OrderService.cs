﻿using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using WebShopBackend.Interfaces;
using WebShopBackend.Models;
using WebShopShared.Models;

namespace WebShopBackend.Services
{
	public class OrderService : IOrderService
	{

		private readonly WebShopDbContext _context;
		private readonly ILogger<OrderService> _logger;
		private readonly IUserService _userService;
		private readonly IProductService _productService;

		public OrderService(WebShopDbContext context, ILogger<OrderService> logger, IUserService userSerivce, IProductService productService)
		{
			_context = context;
			_logger = logger;
			_userService = userSerivce;
			_productService = productService;
		}
		public async Task PostOrderProduct(AddToCartDto addToCartDto, string userEmail)
		{
			var user = await _userService.GetUserByEmail(userEmail);
			var product = await _productService.GetProduct(addToCartDto.Id);

			var pendingOrder = await GetOrCreatePendingOrder(user);

			var existingOrderProduct = await _context.OrderProducts
				.FirstOrDefaultAsync(op => op.OrderId == pendingOrder.Id && op.ProductId == product.Id);

			if (existingOrderProduct != null)
			{
				if (product.Quantity >= addToCartDto.Quantity)
				{
					existingOrderProduct.Quantity += addToCartDto.Quantity;
					await _productService.ChangeQuantity(product.Id, addToCartDto.Quantity);
				}
			}
			else
			{
				if (product.Quantity >= addToCartDto.Quantity)
				{
					var orderProduct = new OrderProduct
					{
						ProductId = product.Id,
						OrderId = pendingOrder.Id,
						Quantity = addToCartDto.Quantity
					};

					_context.OrderProducts.Add(orderProduct);
					await _productService.ChangeQuantity(product.Id, addToCartDto.Quantity);
				}
			}

			await _context.SaveChangesAsync();
		}

		public async Task ChangeOrderStatus(int id)
		{
			var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);

			_logger.LogDebug(order.ToString());

			if(order != null)
			{
				order.Status = OrderStatus.Completed;
				await _context.SaveChangesAsync();
			}
		}

		public async Task<List<OrderProductDetailsDto>> GetOrderProducts(string userEmail)
		{
			var user = await _userService.GetUserByEmail(userEmail);
			var order = await _context.Orders
				.Include(o => o.OrderProducts)
				.ThenInclude(op => op.Product)
				.FirstOrDefaultAsync(o => o.UserId == user.Id && o.Status == OrderStatus.Pending);

			if (order == null)
			{
				_logger.LogInformation("No orders created for this user");
				return new List<OrderProductDetailsDto>(); // Return an empty list if no order is found
			}

			var orderProductDetails = order.OrderProducts.Select(op => new OrderProductDetailsDto
			{
				Product = new ProductDto
				{
					Id = op.Product.Id,
					Name = op.Product.Name,
					Description = op.Product.Description,
					OnSale = op.Product.OnSale,
					Price = op.Product.Price,
					Url = op.Product.Url,
				},
				Quantity = op.Quantity,
				OrderId = op.OrderId
			}).ToList();

			return orderProductDetails;
		}

		public async Task<Order> GetOrCreatePendingOrder(WebshopUser user)
		{
			var pendingOrder = await _context.Orders
				.Where(o => o.UserId == user.Id && o.Status == OrderStatus.Pending)
				.FirstOrDefaultAsync();

			if (pendingOrder == null)
			{
				pendingOrder = new Order
				{
					UserId = user.Id,
					Status = OrderStatus.Pending
				};

				_context.Orders.Add(pendingOrder);
				await _context.SaveChangesAsync();
			}

			return pendingOrder;
		}
	} 
}
