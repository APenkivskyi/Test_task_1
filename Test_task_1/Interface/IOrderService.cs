using TestTask1.Models;

namespace TestTask1.Interface
{
    public interface IOrderService
    {
        Task OrderCreation(Request request, string customerID);
    }
}