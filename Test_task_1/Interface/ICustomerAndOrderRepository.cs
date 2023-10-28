using TestTask1.Models;

namespace TestTask1.Interface
{
    public interface ICustomerAndOrderRepository
    {
        Task CreateAsync(Customers customers);
        Task CreateAsync(Orders orders);
        Task<Customers> FindCustomerAsync(string CustomerName, string CustomerSurname, string CustomerDeliveryAddress);
        Task<Customers> FindCustomerIdAsync(string CustomerId);
        Task<Orders> FindOrdersByCustomerId(string CustomerId);
    }
}