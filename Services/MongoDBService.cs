using Test_task_1.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Test_task_1.Services;

public class MongoDBService
{
    public readonly IMongoCollection<Customers> _customersCollection;
    public readonly IMongoCollection<Orders> _ordersCollection;
    public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _customersCollection = database.GetCollection<Customers>(mongoDBSettings.Value.CollectionNameCustomers);
        _ordersCollection = database.GetCollection<Orders>(mongoDBSettings.Value.CollectionNameOrders);
    }
    public async Task CreateAsync(Customers customers)
    {
        await _customersCollection.InsertOneAsync(customers);
        return;
    }
    public async Task CreateAsync(Orders orders)
    {
        await _ordersCollection.InsertOneAsync(orders);
        return;
    }
    public async Task<List<Customers>> GetAsync()
    {
        return await _customersCollection.Find(new BsonDocument()).ToListAsync();
    }
    public async Task DeleteAsync(string id)
    {
        FilterDefinition<Customers> filter = Builders<Customers>.Filter.Eq("Customer_Id", id);
        await _customersCollection.DeleteOneAsync(filter);
        return;
    }
}
