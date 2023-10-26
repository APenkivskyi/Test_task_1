using System;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Core.Clusters.ServerSelectors;
using TestTask1.Models;
using TestTask1.Services;

namespace TestTask1Tests.Integration_Tests.Services
{
    public class MongoDBServiceTests : IDisposable
    {
        private readonly MongoCustomersAndOrdersRepository _mongoDBService;

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
            _mongoDBService = new MongoCustomersAndOrdersRepository(mongoDBSettings);
        }

        [Fact]
        public async Task AddCustomer_ResultSuccessFindCustomerInDataBase()
        {
            // Arrange
            var customer = new Customers // tworzenie klienta testowego
            {
                CustomerName = "John",
                CustomerSurname = "Doe",
                CustomerDeliveryAddress = "123 Main St"
            };

            // Act
            await _mongoDBService.CreateAsync(customer); // dodajemy klienta testowego do bazy 

            // Assert
            var foundCustomer = await _mongoDBService.FindCustomerAsync(customer.CustomerName, customer.CustomerSurname, customer.CustomerDeliveryAddress); // sprawdzamy czy klient testowy istnieje w bazie
            Assert.NotNull(foundCustomer); // zmienna nie może być pusta
            Assert.Equal(customer.CustomerName, foundCustomer.CustomerName); // porównanie parametrów
            Assert.Equal(customer.CustomerSurname, foundCustomer.CustomerSurname);
            Assert.Equal(customer.CustomerDeliveryAddress, foundCustomer.CustomerDeliveryAddress);
        }

        [Fact]
        public async Task AddOrderInDataBase_ResultSuccessFindOrderInDataBase()
        {
            // Arrange
            var orders = new Orders // zamówienie testowe
            {
                OrderName = "Koszulka",
                OrderDescription = "Koszulka wygodna polo club",
                OrderCustomerId = "3253425346344534"
            };

            // Act
            await _mongoDBService.CreateAsync(orders); // dodajemy zamówienie testowe do bazy

            // Assert
            var foundOrders = await _mongoDBService._ordersCollection
                .Find(x => x.OrderName == orders.OrderName && x.OrderDescription == orders.OrderDescription && x.OrderCustomerId == orders.OrderCustomerId)
                .FirstOrDefaultAsync(); // szukamy zamówienie w bazie danych
            Assert.NotNull(foundOrders); // wartość nie może być pusta
            Assert.Equal(orders.OrderName, foundOrders.OrderName); // porównujemy parametry otrzymanego zamówienia z bazy
            Assert.Equal(orders.OrderDescription, foundOrders.OrderDescription);
            Assert.Equal(orders.OrderCustomerId, foundOrders.OrderCustomerId);
        }

        public void Dispose() // czyścimy po testach baze 
        {
            var client = new MongoClient("mongodb://localhost:27017");
            client.DropDatabase("test_task_1_tests");
        }
    }
}
