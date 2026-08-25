using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Interfaces
{
    public interface IGenericRepository<T> where T :class
    {
       

        Task<T?> GetByIdAsync(int id);

        Task<T> AddAsync(T entity);
       
        Task Update(T entity);
       Task SaveChangesAsync();

        Task Delete(T entity);
    }
}
