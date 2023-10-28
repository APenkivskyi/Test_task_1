using TestTask1.Models;

namespace TestTask1.Interface
{
    public interface ICustomerAndOrderRepository
    {
        Task CreateAsync(Customers customers);
        Task CreateAsync(Orders orders);
        Task<Customers> FindCustomerAsync(string Customer_Name, string Customer_Surname, string Customer_Delivery_Address);
    }
}