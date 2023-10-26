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
        public async Task OrderCreation(Request request, string customerID)
        {
            var orders = new Orders()
            {
                OrderName = request.OrderName,
                OrderDescription = request.OrderDescription,
                OrderPrice = request.OrderPrice,
                OrderCustomerId = customerID,
            };

            await _customerRepository.CreateAsync(orders);
        }
    }
}
