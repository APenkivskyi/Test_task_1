using TestTask1.Models;

namespace TestTask1.Interface
{
    public interface ICustomerService
    {
        Task<string> CreatingClientAsync(Customers customer);
        Task<Customers> FindCustomer(Customers customer);
    }
}