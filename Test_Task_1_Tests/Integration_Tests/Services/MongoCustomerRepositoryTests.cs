using System;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TestTask1.Models;
using TestTask1.Services;

namespace TestTask1Tests.Integration_Tests.Services
{
    public class MongoCustomerRepositoryTests : IDisposable
    {
        private readonly MongoCustomerRepository _customerRepository;

        public MongoCustomerRepositoryTests()
        {
            // Inicjalizacja bazy testowej
            var settings = new MongoDBSettings
            {
                ConnectionURI = "mongodb://localhost:27017",
                DatabaseName = "testTask1Tests",
                CollectionNameCustomers = "customers"
            };
            var mongoDBSettings = Options.Create(settings);
            _customerRepository = new MongoCustomerRepository(mongoDBSettings);
        }

        [Fact]
        public async Task AddCustomerResultSuccessFindCustomerInDataBase()
        {
            // Arrange
            var customer = new Customers // Tworzenie klienta testowego
            {
                CustomerName = "John",
                CustomerSurname = "Doe",
                CustomerDeliveryAddress = "123 Main St",
                AddressOfResidence = "123 Main St",
                CustomerEmail = "John.doe@gmail.com"
            };

            // Act
            await _customerRepository.Create(customer); // Dodajemy klienta testowego do bazy 

            // Assert
            var foundCustomer = await _customerRepository.FindCustomer(customer.CustomerEmail); // Sprawdzamy, czy klient testowy istnieje w bazie
            Assert.NotNull(foundCustomer); // Zmienna nie może być pusta
            Assert.Equal(customer.CustomerName, foundCustomer.CustomerName); // Porównujemy parametry
            Assert.Equal(customer.CustomerSurname, foundCustomer.CustomerSurname);
            Assert.Equal(customer.CustomerDeliveryAddress, foundCustomer.CustomerDeliveryAddress);
            Assert.Equal(customer.AddressOfResidence, foundCustomer.AddressOfResidence);
            Assert.Equal(customer.CustomerEmail, foundCustomer.CustomerEmail);
        }
        // Pozostałe testy bez zmian

        public void Dispose() // Czyścimy po testach bazę
        {
            var client = new MongoClient("mongodb://localhost:27017");
            client.DropDatabase("testTask1Tests");
        }
    }
}
