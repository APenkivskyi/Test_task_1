using Test_task_1.Interface;
using Test_task_1.Models;

namespace Test_task_1.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _mongoDBService;

        public CustomerService(ICustomerRepository mongoDBService)
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
