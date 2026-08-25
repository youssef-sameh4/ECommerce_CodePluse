using AutoMapper;
using ECommerce.Application.Bases;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Services
{
    public class ProductServices : ResponseFactory,IProductServices
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IMapper _mapper;
        public ProductServices(IProductsRepository productsRepository, IMapper mapper)
        {
            _productsRepository = productsRepository;
            _mapper = mapper;
        }

        public async Task<Response<string>> CreateProductAsync(CreateProductDto dto)
        {
            if (dto.Price <= 0)
            {
                return BadRequest<string>("Product price must be greater than zero.");
            }

            if (dto.StockQuantity < 0)
            {
                return BadRequest<string>("Stock quantity cannot be negative.");
            }

            var skuExists = await _productsRepository.SkuExistsAsync(dto.SKU);

            if (skuExists)
            {
                return BadRequest<string>("SKU is already registered.");
            }

            var productmap = _mapper.Map<Product>(dto);

            await _productsRepository.AddAsync(productmap);
            await _productsRepository.SaveChangesAsync();
            return Created("Product Created Successfuly");
        }

        public async Task<Response<string>> DeletProductAsync(int Id)
        {
            var product = await _productsRepository.GetByIdAsync(Id);
            if (product == null)
                return NotFound<string>($"Product with ID {Id} not found.");

            await _productsRepository.Delete(product);
            await _productsRepository.SaveChangesAsync();
            return Deleted<string>();
        }

        public async Task<Response<List<GetAllProductsDTO>>> GetAllProductAsync()
        {
            var products = await _productsRepository.GetAll();
            var response = _mapper.Map<List<GetAllProductsDTO>>(products);
            return Success(response);
        }

        public async Task<Response<GetProductByIdDTO>> GetProductByIdAsync(int Id)
        {
            var product = await _productsRepository.GetByIdAsync(Id);
            if (product == null)
                return NotFound<GetProductByIdDTO>($"Product with ID {Id} not found.");
            var response = _mapper.Map<GetProductByIdDTO> (product);
            return Success(response);
        }

        public async Task<Response<string>> UpdateProductAsync(int Id,UpdateProductDTO product)
        {
            var existing = await _productsRepository.GetByIdAsync(Id);
            if (existing == null)
                return NotFound<string>($"Product with ID {Id} not found.");

            if (product.Price <= 0)
                return BadRequest<string>("Price must be positive.");

            _mapper.Map(product, existing);

            var response=_productsRepository.Update(existing);
            await _productsRepository.SaveChangesAsync();
          return  Success("Product Updated");
        }
    }
}
