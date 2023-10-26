namespace TestTask1.Models
{
    public class MongoDBSettings
    {
        public string ConnectionURI { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string CollectionNameCustomers { get; set; } = null!;
        public string CollectionNameOrders { get; set; } = null!;

    }
}
