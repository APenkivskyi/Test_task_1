﻿using TestTask1.Interface;
using TestTask1.Models;

namespace TestTask1.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerAndOrderRepository _mongoDBService;

        public CustomerService(ICustomerAndOrderRepository mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }
        public async Task<string> CreatingClientAsync(Request request)
        {
            var customers = new Customers()
            {
                Customer_Name = request.Customer_Name,
                Customer_Delivery_Address = request.Customer_Delivery_Address,
                Customer_Surname = request.Customer_Surname
            };

            var existingCustomer = await _mongoDBService.FindCustomerAsync(request.Customer_Name, request.Customer_Surname, request.Customer_Delivery_Address);

            if (existingCustomer == null)
            {
                await _mongoDBService.CreateAsync(customers);
                return customers.Customer_Id;
            }
            else
            {
                return existingCustomer.Customer_Id;
            }
        }
    }
}
