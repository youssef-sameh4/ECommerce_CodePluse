using ECommerce.Application.Interfaces;
using ECommerce.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrustructur.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public async  Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
           
            return entity;
        }

        public async Task Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            
        }

      

        public async  Task<T?> GetByIdAsync(int id)
        {
            return await  _context.Set<T>().FindAsync(id);
           
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _context.Set<T>().Update(entity);
           await _context.SaveChangesAsync();
        }
    }
}
