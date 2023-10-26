using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TestTask1.Models
{
    [BsonIgnoreExtraElements]
    public class Orders
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? OrderId { get; set; }
        [BsonElement("orderName")]
        public string OrderName { get; set; } = null!;
        [BsonElement("orderDescription")]
        public string OrderDescription { get; set; } = null!;
        [BsonElement("orderCustomerId")]
        public string OrderCustomerId { get; set; } = null!;
        [BsonElement("orderPrice")]
        public decimal? OrderPrice { get; set; } = null!;
    }
}
