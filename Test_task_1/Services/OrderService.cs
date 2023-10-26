﻿using TestTask1.Interface;
using TestTask1.Models;

namespace TestTask1.Services
{
    public class OrderService : IOrderService
    {
        private readonly ICustomerAndOrderRepository _customerRepository;

        public OrderService(ICustomerAndOrderRepository mongoDBService)
        {
            _customerRepository = mongoDBService;
        }
        public async Task OrderCreation(Request request, string customerID)
        {
            var orders = new Orders()
            {
                Order_Name = request.Order_Name,
                Order_Description = request.Order_Description,
                Order_Price = request.Order_Price,
                Order_CustomerId = customerID,
            };

            await _customerRepository.CreateAsync(orders);
        }
    }
}
