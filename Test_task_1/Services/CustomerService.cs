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
        public async Task<string> CreatingClientAsync(Request request)
        {
            var customers = new Customers()
            {
                CustomerName = request.CustomerName,
                CustomerDeliveryAddress = request.CustomerDeliveryAddress,
                CustomerSurname = request.CustomerSurname
            };

            var existingCustomer = await _mongoDBService.FindCustomerAsync(request.CustomerName, request.CustomerSurname, request.CustomerDeliveryAddress);

            if (existingCustomer == null)
            {
                await _mongoDBService.CreateAsync(customers);
                return customers.CustomerId;
            }
            else
            {
                return existingCustomer.CustomerId;
            }
        }
        public async Task<Customers> FindCustomer(Request request)
        {
            if(request.CustomerId == null)
            {
                var customers = new Customers()
                {
                    CustomerName = request.CustomerName,
                    CustomerDeliveryAddress = request.CustomerDeliveryAddress,
                    CustomerSurname = request.CustomerSurname
                };

                var existingCustomer = await _mongoDBService.FindCustomerAsync(request.CustomerName, request.CustomerSurname, request.CustomerDeliveryAddress);
                if(existingCustomer != null)
                {
                    return existingCustomer;
                }
                return null;
            }
            else
            {
                var existingCustomer = await _mongoDBService.FindCustomerIdAsync(request.CustomerId);
                if(existingCustomer != null)
                {
                    return existingCustomer;
                }
                return null;
            }
            
        }
    }
}
