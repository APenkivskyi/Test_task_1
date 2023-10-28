using TestTask1.Models;

namespace TestTask1.Interface
{
    public interface ICustomerService
    {
        Task<string> CreatingClientAsync(Request request);
        Task<Customers> FindCustomer(Request request);
    }
}