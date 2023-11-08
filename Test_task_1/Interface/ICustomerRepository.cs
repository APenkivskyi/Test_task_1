using TestTask1.Models;

namespace TestTask1.Interface
{
    public interface ICustomerRepository
    {
        Task CreateAsync(Customers customers);
        Task<Customers> FindCustomerAsync(string CustomerEmail);
        Task<Customers> FindCustomerIdAsync(string CustomerId);
    }
}