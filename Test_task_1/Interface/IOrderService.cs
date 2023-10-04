using Test_task_1.Models;

namespace Test_task_1.Interface
{
    public interface IOrderService
    {
        Task OrderCreation(Request request, string customerID);
    }
}