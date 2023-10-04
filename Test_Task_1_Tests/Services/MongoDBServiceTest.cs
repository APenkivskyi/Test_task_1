using System;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Core.Clusters.ServerSelectors;
using Test_task_1.Services;
using Test_task_1.Models;

namespace Test_task_1.Tests
{
    public class MongoDBServiceTests : IDisposable
    {
        private readonly MongoCustomerRepository _mongoDBService;

        public MongoDBServiceTests()
        {
            // Deklaracja bazy testowej
            var settings = new MongoDBSettings
            {
                ConnectionURI = "mongodb://localhost:27017",
                DatabaseName = "test_task_1_tests",
                CollectionNameCustomers = "customers",
                CollectionNameOrders = "orders"
            };
            var mongoDBSettings = Options.Create(settings);
            _mongoDBService = new MongoCustomerRepository(mongoDBSettings);
        }

        [Fact]
        public async Task AddCustomer_ResultSuccessFindCustomerInDataBase()
        {
            // Arrange
            var customer = new Customers // tworzenie klienta testowego
            {
                Customer_Name = "John",
                Customer_Surname = "Doe",
                Customer_Delivery_Address = "123 Main St"
            };

            // Act
            await _mongoDBService.CreateAsync(customer); // dodajemy klienta testowego do bazy 

            // Assert
            var foundCustomer = await _mongoDBService.FindCustomerAsync(customer.Customer_Name, customer.Customer_Surname, customer.Customer_Delivery_Address); // sprawdzamy czy klient testowy istnieje w bazie
            Assert.NotNull(foundCustomer); // zmienna nie może być pusta
            Assert.Equal(customer.Customer_Name, foundCustomer.Customer_Name); // porównanie parametrów
            Assert.Equal(customer.Customer_Surname, foundCustomer.Customer_Surname);
            Assert.Equal(customer.Customer_Delivery_Address, foundCustomer.Customer_Delivery_Address);
        }

        [Fact]
        public async Task AddOrderInDataBase_ResultSuccessFindOrderInDataBase()
        {
            // Arrange
            var orders = new Orders // zamówienie testowe
            {
                Order_Name = "Koszulka",
                Order_Description = "Koszulka wygodna polo club",
                Order_CustomerId = "3253425346344534"
            };

            // Act
            await _mongoDBService.CreateAsync(orders); // dodajemy zamówienie testowe do bazy

            // Assert
            var foundOrders = await _mongoDBService._ordersCollection
                .Find(x => x.Order_Name == orders.Order_Name && x.Order_Description == orders.Order_Description && x.Order_CustomerId == orders.Order_CustomerId)
                .FirstOrDefaultAsync(); // szukamy zamówienie w bazie danych
            Assert.NotNull(foundOrders); // wartość nie może być pusta
            Assert.Equal(orders.Order_Name, foundOrders.Order_Name); // porównujemy parametry otrzymanego zamówienia z bazy
            Assert.Equal(orders.Order_Description, foundOrders.Order_Description);
            Assert.Equal(orders.Order_CustomerId, foundOrders.Order_CustomerId);
        }

        public void Dispose() // czyścimy po testach baze 
        {
            var client = new MongoClient("mongodb://localhost:27017");
            client.DropDatabase("test_task_1_tests");
        }
    }
}
