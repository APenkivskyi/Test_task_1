
namespace Test_task_1.Models
{
    public class Request
    {
        public string Customer_Name { get; set; } = null!;
        public string Customer_Surname { get; set; } = null!;
        public string Customer_Delivery_Address { get; set; } = null!;
        public string Order_Name { get; set; }
        public string Order_Description { get; set; }
        public int Order_Price { get; set; }
    }
}
