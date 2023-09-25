using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Test_task_1.Controllers;
using Test_task_1.Models;
using Test_task_1.Services;

namespace Test_Task_1_Tests.Controller
{
    public class CustomerProductsControllerTests
    {
        private Mock<IMongoDBService> _mockMongoDBService;
        private CustomerProductsController _controller;

        [SetUp]
        public void Setup()
        {
            _mockMongoDBService = new Mock<IMongoDBService>();
            _controller = new CustomerProductsController(_mockMongoDBService.Object);
        }

        [Test]
        public async Task Post_ShouldInsertOrderAndCustomer()
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

            // Sprawdzenie, czy metoda CreateAsync została wywołana z odpowiednimi danymi
            _mockMongoDBService.Verify(x => x.CreateAsync(It.IsAny<Customers>()), Times.Once);
        }
    }
}
