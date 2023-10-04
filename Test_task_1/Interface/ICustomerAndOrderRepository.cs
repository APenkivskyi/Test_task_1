using Test_task_1.Models;

namespace Test_task_1.Services
{
    public interface ICustomerAndOrderRepository
    {
        Task CreateAsync(Customers customers);
        Task CreateAsync(Orders orders);
        Task<Customers> FindCustomerAsync(string Customer_Name, string Customer_Surname, string Customer_Delivery_Address);
    }
}