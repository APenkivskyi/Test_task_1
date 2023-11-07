using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace TestTask1.Models;

public class Customers
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? CustomerId { get; set; }
    [BsonElement("customerName")]
    public string CustomerName { get; set; } = null!;
    [BsonElement("customerSurname")]
    public string CustomerSurname { get; set; } = null!;
    [BsonElement("customerDeliveryAddress")]
    public string CustomerDeliveryAddress { get; set; } = null!;
    [BsonElement("addressOfResidence")]
    public string? AddressOfResidence { get; set;}
}

