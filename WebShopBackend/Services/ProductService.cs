﻿using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using WebShopBackend.Model;
using WebShopShared.Models;
using WebShopShared.Interfaces;

namespace WebShopBackend.Services
{
	public class ProductService : IProductService
	{
		private WebShopDbContext _context;
		private ILogger<ProductService> _logger;
		public ProductService(WebShopDbContext context, ILogger<ProductService> logger)
		{
			_context = context;
			_logger = logger;
		}
		public async Task<List<Product>> GetProducts()
		{
			return await _context.Products.ToListAsync();
		}

		public async Task<Product> GetProduct(int id)
		{
			var product = await _context.Products.FirstOrDefaultAsync(p => p.id == id);

			if (product == null)
			{
				_logger.LogError("No product found with id {ProductId}", id);
				throw new KeyNotFoundException($"Product with id {id} not found.");
			}
			return product;
		}
	}
}
