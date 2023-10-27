using TestTask1.Interface;
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
        public async Task<string> CreatingClientAsync(Customers customer)
        {
            var existingCustomer = await _mongoDBService.FindCustomerAsync(customer.CustomerName, customer.CustomerSurname, customer.CustomerDeliveryAddress);

            if (existingCustomer == null)
            {
                await _mongoDBService.CreateAsync(customer);
                return customer.CustomerId;
            }
            else
            {
                return existingCustomer.CustomerId;
            }
        }
    }
}
