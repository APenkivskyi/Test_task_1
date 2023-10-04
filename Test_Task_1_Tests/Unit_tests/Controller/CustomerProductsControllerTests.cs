using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Test_task_1.Controllers;
using Test_task_1.Models;
using Test_task_1.Services;

namespace Test_Task_1_Tests.Unit_tests.Controller
{
    public class CustomerProductsControllerTests
    {
        private Mock<ICustomerRepository> _mockMongoDBService;
        private CustomerProductsController _controller;

        [SetUp]
        public void Setup()
        {
            _mockMongoDBService = new Mock<ICustomerRepository>();
            _controller = new CustomerProductsController(_mockMongoDBService.Object);
        }

        [Test]
        public async Task Post_ShouldInsertOrderAndCustomer() // Sprawdzamy dodawanie klienta i zamówienia do bazy
        {
            // Przygotowanie danych testowych
            var customerName = "Adam";
            var customerSurname = "Kowalski";
            var customerDeliveryAddress = "Beach street";
            var orderName = "Sample Order";
            var orderDescription = "Sample Description";
            var orderPrice = 100;

            _mockMongoDBService.Setup(x => x.FindCustomerAsync(customerName, customerSurname, customerDeliveryAddress))
                .ReturnsAsync((Customers)null); // Symulujemy brak istniejącego klienta

            // Wywołanie metody Post
            var result = await _controller.Post(customerName, customerSurname, customerDeliveryAddress, orderName, orderDescription, orderPrice);

            // Sprawdzenie, czy wynik to OkObjectResult
            Assert.IsInstanceOf<OkObjectResult>(result);

            // Sprawdzenie, czy metoda CreateAsync została wywołana raz dla klienta
            _mockMongoDBService.Verify(x => x.CreateAsync(It.IsAny<Customers>()), Times.Once);

            // Sprawdzenie, czy metoda CreateAsync została wywołana raz dla zamówienia
            _mockMongoDBService.Verify(x => x.CreateAsync(It.IsAny<Orders>()), Times.Once);
        }
        [Test]
        public async Task Post_ShouldNotAddCustomerIfItAlreadyExists() // sprawdzamy czy klient nie będzie się dodawał jeżeli istnieje w bazie
        {
            // Przygotowanie danych testowych
            var customerName = "Adam";
            var customerSurname = "Kowalski";
            var customerDeliveryAddress = "Beach street";
            var orderName = "Sample Order";
            var orderDescription = "Sample Description";
            var orderPrice = 100;

            // Symulacja istniejącego klienta
            var existingCustomer = new Customers
            {
                Customer_Name = customerName,
                Customer_Surname = customerSurname,
                Customer_Delivery_Address = customerDeliveryAddress,
                Customer_Id = "2142352314321342"
            };

            _mockMongoDBService.Setup(x => x.FindCustomerAsync(customerName, customerSurname, customerDeliveryAddress))
                .ReturnsAsync(existingCustomer); // Symulujemy istniejącego klienta

            // Wywołanie metody Post
            var result = await _controller.Post(customerName, customerSurname, customerDeliveryAddress, orderName, orderDescription, orderPrice);

            // Sprawdzenie, czy wynik to OkObjectResult
            Assert.IsInstanceOf<OkObjectResult>(result);

            // Sprawdzenie, czy metoda CreateAsync dla klienta nie została wywołana
            _mockMongoDBService.Verify(x => x.CreateAsync(It.IsAny<Customers>()), Times.Never);

            // Sprawdzenie, czy metoda CreateAsync została wywołana raz dla zamówienia
            _mockMongoDBService.Verify(x => x.CreateAsync(It.IsAny<Orders>()), Times.Once);
        }
    }
}
