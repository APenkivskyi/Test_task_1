using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Test_task_1.Models
{
    [BsonIgnoreExtraElements]
    public class Orders
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Order_Id {  get; set; }
        [BsonElement("order_name")]
        public string Order_Name { get; set; }
        [BsonElement("order_description")]
        public string Order_Description { get; set; }
        [BsonElement("order_customerId")]
        public string Order_CustomerId { get; set; }
        [BsonElement("order_price")]
        public int Order_Price { get; set; }
    }
}
