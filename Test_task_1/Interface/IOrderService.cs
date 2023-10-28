using TestTask1.Models;

namespace TestTask1.Interface
{
    public interface IOrderService
    {
        Task<string> OrderCreation(Request request, string customerID);
        Task<Orders> FindOrders(string OrderCustomerId);
    }
}