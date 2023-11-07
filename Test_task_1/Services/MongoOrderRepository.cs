using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using TestTask1.Models;
using TestTask1.Interface;
using System.Collections.Generic;

namespace TestTask1.Services
{
    public class MongoOrderRepository : IOrderRepository
    {
        private readonly IMongoCollection<Orders> _ordersCollection;
        public MongoOrderRepository(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _ordersCollection = database.GetCollection<Orders>(mongoDBSettings.Value.CollectionNameOrders);
        }
        public async Task CreateAsync(Orders orders)
        {
            await _ordersCollection.InsertOneAsync(orders);
            return;
        }
        public async Task<List<Orders>> FindOrdersByCustomerId(string customerId)
        {
            var existingOrders = await _ordersCollection
                .Find(x => x.OrderCustomerId == customerId)
                .ToListAsync();
            return existingOrders;
        }
    }
}
