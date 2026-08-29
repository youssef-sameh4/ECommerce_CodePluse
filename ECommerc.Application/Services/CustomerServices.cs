using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Services
{
    public class CustomerServices :  ICustomerServices
    {
        private readonly ICustomerRepository _customerRepository;
       

        public CustomerServices(ICustomerRepository customerRepository)
        {
            this._customerRepository = customerRepository;
           
        }

        public async Task<string> CreateCustomerAsync(Customer customer)
        {
            

            var emailExists = await _customerRepository.EmailExists(customer.Email);
            if (emailExists)
            {
                return "Email registered";
            }
            await _customerRepository.AddAsync(customer);
            await _customerRepository.SaveChangesAsync();
            return "Success";
        }

        public async Task<Customer?> GetCustomerByIdAsync(int Id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(Id);
           
            return customer;
        }

        public async Task<string> UpgradeToVipAsync(int customerId)
        {

            var customer = await _customerRepository.GetByIdAsync(customerId);
            if (customer == null)
            {
                return "Customer Null";

            }
            var totalSpent =
                await _customerRepository.GetTotalSpentByCustomerIdAsync(customerId);
        

            if (totalSpent < 500m)
                return "totalSpent less than 500";

            customer.IsVip = true;

            await _customerRepository.SaveChangesAsync();

            return "Success";
        }
    }
}
