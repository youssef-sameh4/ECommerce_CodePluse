using AutoMapper;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IMapper _mapper;
        public ProductServices(IProductsRepository productsRepository, IMapper mapper)
        {
            _productsRepository = productsRepository;
            _mapper = mapper;
        }

        public async Task<string> CreateProductAsync(Product product)
        {
            if (product.Price <= 0)
            {
                return "Product price Fail.";
            }

            if (product.StockQuantity < 0)
            {
                return "Stock quantity fail.";
            }

            var skuExists = await _productsRepository.SkuExistsAsync(product.SKU);

            if (skuExists)
            {
                return "SKU fail.";
            }

         

            await _productsRepository.AddAsync(product);
            await _productsRepository.SaveChangesAsync();
            return "Success";
        }

        public async Task<string> DeletProductAsync(int Id)
        {
            var product = await _productsRepository.GetByIdAsync(Id);
            if (product == null)
                return "Null";
            await _productsRepository.Delete(product);
            await _productsRepository.SaveChangesAsync();
            return "Success";
        }

        public async Task<List<Product>> GetAllProductAsync()
        {
            var products = await _productsRepository.GetAll();
           
            return products;
        }

        public async Task<Product> GetProductByIdAsync(int Id)
        {
            var product = await _productsRepository.GetByIdAsync(Id);
            if (product == null)
            {
                return null;
            }
            return product;
        }

        public async Task<string> UpdateProductAsync(Product product)
        {
            var existing = await _productsRepository.GetByIdAsync(product.Id);
            if (existing == null)
                return "Null";

            if (product.Price <= 0)
            {
                return "Price Fail";
            }
          

            await _productsRepository.Update(existing);
            await _productsRepository.SaveChangesAsync();
          return  "Success";
        }
    }
}
