using Microsoft.AspNetCore.Http.HttpResults;
using TestTask1.Interface;
using TestTask1.Models;

namespace TestTask1.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;

        public OrderService(IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
        }
        public async Task<string?> OrderCreation(Orders order)
        {
            var clientInDatabase = await _customerRepository.FindCustomerId(order.OrderCustomerId);
            if (clientInDatabase != null)
            {
                await _orderRepository.CreateAsync(order);
                return (order.OrderId);
            }
            return null;
        }
        public async Task<List<Orders>> FindOrders(string customerId)
        {
            var resultCustomer = await _customerRepository.FindCustomerId(customerId);
            if (resultCustomer != null)
            {
                var result = _orderRepository.FindOrdersByCustomerId(customerId);
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
