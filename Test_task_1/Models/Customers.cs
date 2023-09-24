using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Test_task_1.Models;

    public class Customers
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Customer_Id { get; set; } 
        [BsonElement("customer_name")]
        public string Customer_Name { get; set; } = null!;
        [BsonElement("customer_surname")]
        public string Customer_Surname { get; set; } = null!;
        [BsonElement("customer_delivery_Address")]
        public string Customer_Delivery_Address { get; set; } = null!;
    }

