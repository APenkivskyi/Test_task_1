using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using TestTask1.Models;
using TestTask1.Interface;
using System.Collections.Generic;

namespace TestTask1.Services;

public class MongoCustomerRepository : ICustomerRepository
{
    private readonly IMongoCollection<Customers> _customersCollection;
    public MongoCustomerRepository(IOptions<MongoDBSettings> mongoDBSettings)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _customersCollection = database.GetCollection<Customers>(mongoDBSettings.Value.CollectionNameCustomers);
    }
    public async Task CreateAsync(Customers customers)
    {
        await _customersCollection.InsertOneAsync(customers);
        return;
    }
    public async Task<Customers> FindCustomerAsync(string CustomerName, string CustomerSurname, string CustomerDeliveryAddress)
    {
        var existingCustomer = await _customersCollection
            .Find(x => x.CustomerName == CustomerName && x.CustomerSurname == CustomerSurname && x.CustomerDeliveryAddress == CustomerDeliveryAddress)
            .FirstOrDefaultAsync();

        return existingCustomer;
    }
    public async Task<Customers> FindCustomerIdAsync(string CustomerId)
    {
        var existingCustomer = await _customersCollection
            .Find(x => x.CustomerId == CustomerId)
            .FirstOrDefaultAsync();

        return existingCustomer;
    }
}
