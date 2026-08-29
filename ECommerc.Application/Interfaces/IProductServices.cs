using AutoMapper.Execution;
using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Interfaces
{
    public interface IProductServices
    {
        Task<List<Product>> GetAllProductAsync();
        Task<Product> GetProductByIdAsync(int Id);
         Task<string> CreateProductAsync(Product product);
        Task<string> UpdateProductAsync(Product product);
        Task<string> DeletProductAsync(int Id);
    }
}
