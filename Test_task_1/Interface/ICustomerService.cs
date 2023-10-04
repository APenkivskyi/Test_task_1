using Test_task_1.Models;

namespace Test_task_1.Interface
{
    public interface ICustomerService
    {
        Task<string> CreatingClientAsync(Request request);
    }
}