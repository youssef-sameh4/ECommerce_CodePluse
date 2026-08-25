using ECommerce.Application.Bases;
using ECommerce.Application.Interfaces;
using ECommerce.DAL.Context;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrustructur.Repositories
{
    public class ProductsRepository : GenericRepository<Product>, IProductsRepository
    {
        private readonly DbSet<Product> _products;

        public ProductsRepository(AppDbContext context) : base(context)
        {
            _products = context.Set<Product>();
        }
        public async Task<List<Product>> GetAll()
        {
            return await _products.ToListAsync();
        }

        public async Task<bool> SkuExistsAsync(string sku)
        {
            return await _products
        .AnyAsync(p => p.SKU.ToLower() == sku.ToLower());
        }
    }
}
