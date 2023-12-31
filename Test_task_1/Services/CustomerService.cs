﻿using TestTask1.Interface;
using TestTask1.Models;

namespace TestTask1.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository mongoDBService)
        {
            _customerRepository = mongoDBService;
        }
        public async Task<string> CreatingClientAsync(Customers customer)
        {
            var existingCustomer = await _customerRepository.FindCustomer(customer.CustomerEmail);

            if (existingCustomer == null)
            {
                await _customerRepository.Create(customer);
                return customer.CustomerId;
            }
            else
            {
                return existingCustomer.CustomerId;
            }
        }
        public async Task<Customers> FindCustomer(Customers customer)
        {
            if(customer.CustomerId == null)
            {
                var existingCustomer = await _customerRepository.FindCustomer(customer.CustomerEmail);
                if(existingCustomer != null)
                {
                    return existingCustomer;
                }
                return null;
            }
            else
            {
                var existingCustomer = await _customerRepository.FindCustomerId(customer.CustomerId);
                if(existingCustomer != null)
                {
                    return existingCustomer;
                }
                return null;
            }
            
        }
    }
}
