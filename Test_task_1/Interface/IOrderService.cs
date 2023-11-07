using TestTask1.Models;

namespace TestTask1.Interface
{
    public interface IOrderService
    {
        Task<string> OrderCreation(Orders order);
        Task<List<Orders>> FindOrders(string OrderCustomerId);
    }
}