using System;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TestTask1.Models;
using TestTask1.Services;

namespace TestTask1Tests.Integration_Tests.Services
{
    public class MongoDBServiceTests : IDisposable
    {
        private readonly MongoCustomersAndOrdersRepository _mongoDBService;

        public MongoDBServiceTests()
        {
            // Inicjalizacja bazy testowej
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
            var customer = new Customers // Tworzenie klienta testowego
            {
                CustomerName = "John",
                CustomerSurname = "Doe",
                CustomerDeliveryAddress = "123 Main St"
            };

            // Act
            await _mongoDBService.CreateAsync(customer); // Dodajemy klienta testowego do bazy 

            // Assert
            var foundCustomer = await _mongoDBService.FindCustomerAsync(customer.CustomerName, customer.CustomerSurname, customer.CustomerDeliveryAddress); // Sprawdzamy, czy klient testowy istnieje w bazie
            Assert.NotNull(foundCustomer); // Zmienna nie może być pusta
            Assert.Equal(customer.CustomerName, foundCustomer.CustomerName); // Porównujemy parametry
            Assert.Equal(customer.CustomerSurname, foundCustomer.CustomerSurname);
            Assert.Equal(customer.CustomerDeliveryAddress, foundCustomer.CustomerDeliveryAddress);
        }

        [Fact]
        public async Task AddOrderInDataBaseResultSuccessFindOrderInDataBase()
        {
            // Arrange
            var orders = new Orders // Zamówienie testowe
            {
                OrderName = "Koszulka",
                OrderDescription = "Koszulka wygodna polo club",
                OrderCustomerId = "3253425346344534"
            };

            // Act
            await _mongoDBService.CreateAsync(orders); // Dodajemy zamówienie testowe do bazy

            // Assert
            var foundOrders = await _mongoDBService.FindOrdersByCustomerId(orders.OrderCustomerId); // Korzystamy z metody FindOrdersByCustomerId
            Assert.NotNull(foundOrders); // Wartość nie może być pusta
            Assert.Single(foundOrders); // Sprawdź, czy lista zawiera tylko jedno zamówienie
            var foundOrder = foundOrders[0];
            Assert.Equal(orders.OrderName, foundOrder.OrderName); // Porównujemy parametry otrzymanego zamówienia z bazy
            Assert.Equal(orders.OrderDescription, foundOrder.OrderDescription);
            Assert.Equal(orders.OrderCustomerId, foundOrder.OrderCustomerId);
        }

        // Pozostałe testy bez zmian

        public void Dispose() // Czyścimy po testach bazę
        {
            var client = new MongoClient("mongodb://localhost:27017");
            client.DropDatabase("test_task_1_tests");
        }
    }
}
