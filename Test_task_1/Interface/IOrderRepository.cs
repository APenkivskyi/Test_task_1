using TestTask1.Models;

namespace TestTask1.Interface
{
    public interface IOrderRepository
    {
        Task CreateAsync(Orders orders);
        Task<List<Orders>> FindOrdersByCustomerId(string CustomerId);
    }
}
