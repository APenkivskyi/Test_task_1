using Microsoft.AspNetCore.Http.HttpResults;
using TestTask1.Interface;
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
        public async Task<string?> OrderCreation(Request request, string customerID)
        {
            var clientInDatabase = _customerRepository.FindCustomerIdAsync(customerID);
            if(clientInDatabase.Result != null)
            {
                var orders = new Orders()
                {
                    OrderName = request.OrderName,
                    OrderDescription = request.OrderDescription,
                    OrderPrice = request.OrderPrice,
                    OrderCustomerId = customerID,
                };
                await _customerRepository.CreateAsync(orders);
                return (orders.OrderId);
            }
            return null;
        }
        public async Task<List<Orders>> FindOrders(string OrderCustomerId)
        {
            var result = _customerRepository.FindOrdersByCustomerId(OrderCustomerId);
            if(result != null)
            {
                return await result;
            }
            return null;
        }
    }
}
