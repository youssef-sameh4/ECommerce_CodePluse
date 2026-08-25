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
    public class CustomerServices : ResponseFactory, ICustomerServices
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerServices(ICustomerRepository customerRepository, IMapper mapper)
        {
            this._customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<Response<string>> CreateCustomerAsync(CreateCustomerDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.FullName))
                return BadRequest<string>("Full name is required.");

            if (string.IsNullOrWhiteSpace(dto.Email) || !dto.Email.Contains("@"))
                return BadRequest<string>("A valid email address is required.");

            var emailExists = await _customerRepository.EmailExists(dto.Email);
            if (emailExists)
            {
                return BadRequest<string>("Email is already registered.");
            }
            //mapping
            
            var customer = _mapper.Map<Customer>(dto);

            await _customerRepository.AddAsync(customer);
            await _customerRepository.SaveChangesAsync();
            return Created("Customer Created Successfuly");
        }

        public async Task<Response<GetCustomerByIdDTO>> GetCustomerByIdAsync(int Id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(Id);
            if (customer == null)
                return NotFound<GetCustomerByIdDTO>($"Customer with ID {Id} not found.");
            var customermap = _mapper.Map<GetCustomerByIdDTO>(customer);
           
            return Success(customermap);
        }

        public async Task<Response<string>> UpgradeToVipAsync(int customerId)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(customerId);

            if (customer == null)
                return NotFound<string>("Customer not found.");

            var totalSpent =
                await _customerRepository.GetTotalSpentByCustomerIdAsync(customerId);

            if (totalSpent < 500m)
                return BadRequest<string>("Customer does not qualify for VIP.");

            customer.IsVip = true;

            await _customerRepository.SaveChangesAsync();

            return Success("Customer upgraded to VIP successfully.");
        }
    }
}
