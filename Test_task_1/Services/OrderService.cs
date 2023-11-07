using Microsoft.AspNetCore.Http.HttpResults;
using TestTask1.Interface;
using TestTask1.Models;

namespace TestTask1.Services
{
    public class OrderService : IOrderService
    {
        private readonly ICustomerRepository _customerRepository;

        public OrderService(ICustomerRepository mongoDBService)
        {
            _customerRepository = mongoDBService;
        }
        public async Task<string?> OrderCreation(Orders order)
        {
            var clientInDatabase = _customerRepository.FindCustomerIdAsync(order.OrderCustomerId);
            if(clientInDatabase.Result != null)
            {
                await _customerRepository.CreateAsync(order);
                return (order.OrderId);
            }
            return null;
        }
        public async Task<List<Orders>> FindOrders(string customerId)
        {
            var resultCustomer = await _customerRepository.FindCustomerIdAsync(customerId);
            if(resultCustomer != null)
            {
                var result = _customerRepository.FindOrdersByCustomerId(customerId);
                if (result.Result != null)
                {
                    return await result;
                }
                return null;
            }
            return null;
        }
    }
}
