using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using TestTask1.Models;
using TestTask1.Interface;

namespace TestTask1.Services;

public class MongoCustomersAndOrdersRepository : ICustomerAndOrderRepository
{
    public readonly IMongoCollection<Customers> _customersCollection;
    public readonly IMongoCollection<Orders> _ordersCollection;
    public MongoCustomersAndOrdersRepository(IOptions<MongoDBSettings> mongoDBSettings)
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
    public async Task<Customers> FindCustomerAsync(string Customer_Name, string Customer_Surname, string Customer_Delivery_Address)
    {
        var existingCustomer = await _customersCollection
            .Find(x => x.Customer_Name == Customer_Name && x.Customer_Surname == Customer_Surname && x.Customer_Delivery_Address == Customer_Delivery_Address)
            .FirstOrDefaultAsync();

        return existingCustomer;
    }
}
