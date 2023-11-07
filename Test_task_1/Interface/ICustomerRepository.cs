﻿using TestTask1.Models;

namespace TestTask1.Interface
{
    public interface ICustomerAndOrderRepository
    {
        Task CreateAsync(Customers customers);
        Task<Customers> FindCustomerAsync(string CustomerName, string CustomerSurname, string CustomerDeliveryAddress);
        Task<Customers> FindCustomerIdAsync(string CustomerId);
    }
}