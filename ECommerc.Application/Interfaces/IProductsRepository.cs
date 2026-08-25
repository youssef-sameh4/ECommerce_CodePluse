using ECommerce.Application.Bases;
using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Interfaces
{
    public interface IProductsRepository: IGenericRepository<Product>
    {
        Task<bool> SkuExistsAsync(string sku);
        Task<List<Product>> GetAll();
    }
}
