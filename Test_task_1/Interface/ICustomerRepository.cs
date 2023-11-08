using TestTask1.Models;

namespace TestTask1.Interface
{
    public interface ICustomerRepository
    {
        Task Create(Customers customers);
        Task<Customers> FindCustomer(string CustomerEmail);
        Task<Customers> FindCustomerId(string CustomerId);
    }
}