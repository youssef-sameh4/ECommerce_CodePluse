using AutoMapper.Execution;
using ECommerce.Application.Bases;
using ECommerce.Application.DTOs;
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
        Task<Response<List<GetAllProductsDTO>>> GetAllProductAsync();
        Task<Response<GetProductByIdDTO>> GetProductByIdAsync(int Id);
        Task<Response<string>> CreateProductAsync(CreateProductDto dto);
        Task<Response<string>> UpdateProductAsync(int Id, UpdateProductDTO product);
        Task<Response<string>> DeletProductAsync(int Id);
    }
}
