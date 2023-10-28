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
        public async Task AddCustomerResultSuccessFindCustomerInDataBase()
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
        public async Task AddOrderInDataBaseResultSuccessFindOrderInDataBase()
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
        [Fact]
        public async Task FindCustomerInDatabaseResultSuccess()
        {
            // Arrange
            var customer = new Customers // tworzenie klienta testowego
            {
                CustomerName = "John",
                CustomerSurname = "Doe",
                CustomerDeliveryAddress = "123 Main St"
            };
            await _mongoDBService.CreateAsync(customer);
            // Act
            var result = await _mongoDBService.FindCustomerIdAsync(customer.CustomerId);
            // Assert
            Assert.Equal(customer.CustomerName, result.CustomerName);
            Assert.Equal(customer.CustomerSurname, result.CustomerSurname);
            Assert.Equal(customer.CustomerDeliveryAddress, result.CustomerDeliveryAddress);
        }
        [Fact]
        public async Task FindOrdersByCustomerIdResultSuccess()
        {
            // Arrange
            var order1 = new Orders // zamówienie testowe
            {
                OrderName = "Koszulka",
                OrderDescription = "Koszulka wygodna polo club",
                OrderCustomerId = "3253425346344534"
            };
            var order2 = new Orders // zamówienie testowe
            {
                OrderName = "Spodnie",
                OrderDescription = "Spodnie BOSS",
                OrderCustomerId = "3253425346344534"
            };
            var order3 = new Orders // zamówienie testowe
            {
                OrderName = "Czapka",
                OrderDescription = "Koszulka club",
                OrderCustomerId = "3253425346344534"
            };
            await _mongoDBService.CreateAsync(order1);
            await _mongoDBService.CreateAsync(order2);
            await _mongoDBService.CreateAsync(order3);
            // Act
            var result = await _mongoDBService._ordersCollection.Find(x => x.OrderCustomerId == order1.OrderCustomerId).ToListAsync();
            // Assert
            Assert.Equal(result[0].OrderName, order1.OrderName);
            Assert.Equal(result[0].OrderDescription, order1.OrderDescription);
            Assert.Equal(result[1].OrderName, order2.OrderName);
            Assert.Equal(result[1].OrderDescription, order2.OrderDescription);
            Assert.Equal(result[2].OrderName, order3.OrderName);
            Assert.Equal(result[2].OrderDescription, order3.OrderDescription);
        }
        public void Dispose() // czyścimy po testach baze 
        {
            var client = new MongoClient("mongodb://localhost:27017");
            client.DropDatabase("test_task_1_tests");
        }
    }
}
