using System;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TestTask1.Models;
using TestTask1.Services;

namespace TestTask1Tests.Integration_Tests.Services
{
    public class MongoOrderRepositoryTests : IDisposable
    {
        private readonly MongoOrderRepository _orderRepository;

        public MongoOrderRepositoryTests()
        {
            // Inicjalizacja bazy testowej
            var settings = new MongoDBSettings
            {
                ConnectionURI = "mongodb://localhost:27017",
                DatabaseName = "testTask1Tests",
                CollectionNameOrders = "orders"
            };
            var mongoDBSettings = Options.Create(settings);
            _orderRepository = new MongoOrderRepository(mongoDBSettings);
        }

        [Fact]
        public async Task AddOrderInDataBaseResultSuccessFindOrderInDataBase()
        {
            // Arrange
            var orders = new Orders // Zamówienie testowe
            {
                OrderName = "Koszulka",
                OrderDescription = "Koszulka wygodna polo club",
                OrderCustomerId = "3253425346344534",
                OrderPrice = (decimal?)243532.324
            };

            // Act
            await _orderRepository.CreateAsync(orders); // Dodajemy zamówienie testowe do bazy

            // Assert
            var foundOrders = await _orderRepository.FindOrdersByCustomerId(orders.OrderCustomerId); // Korzystamy z metody FindOrdersByCustomerId
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
            client.DropDatabase("testTask1Tests");
        }
    }
}
