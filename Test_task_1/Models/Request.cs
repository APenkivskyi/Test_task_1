namespace TestTask1.Models
{
    public class Request
    {
        public string CustomerName { get; set; } = null!;
        public string CustomerSurname { get; set; } = null!;
        public string CustomerDeliveryAddress { get; set; } = null!;
        public string OrderName { get; set; } = null!;
        public string OrderDescription { get; set; } = null!;
        public decimal? OrderPrice { get; set; } = null!;
    }
}
