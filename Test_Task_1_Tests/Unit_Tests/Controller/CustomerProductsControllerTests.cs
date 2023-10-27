using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using TestTask1.Services;
using TestTask1.Controllers;
using TestTask1.Interface;
using TestTask1.Models;
using Castle.Core.Resource;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace TestTask1Tests.Unit_Tests.Controller
{
    public class CustomerProductsControllerTests
    {
        private Mock<ICustomerService> _mockICustomerService;
        private Mock<IOrderService> _mockIOrderService;
        private CustomerProductsController _controller;

        [SetUp]
        public void Setup()
        {
            _mockICustomerService = new Mock<ICustomerService>();
            _mockIOrderService = new Mock<IOrderService>();
            _controller = new CustomerProductsController(_mockICustomerService.Object, _mockIOrderService.Object);
        }
        [Test]
        public async Task Post_ShouldInsertOrderAndCustomer() // Sprawdzamy dodawanie klienta i zamówienia do bazy
        {
            // Przygotowanie danych testowych
            Request request = new Request
            {
                CustomerName = "Adam",
                CustomerSurname = "Kowalski",
                CustomerDeliveryAddress = "Beach street",
                OrderName = "Sample Order",
                OrderDescription = "Sample Description",
                OrderPrice = 100
            };
            Customers fakeCustomer = new Customers
            {
                CustomerName = "Adam",
                CustomerSurname = "Kowalski",
                CustomerDeliveryAddress = "Beach street",
            };
            _mockICustomerService.Setup(x => x.CreatingClientAsync(fakeCustomer)).ReturnsAsync("ewrjn435nj34j5n");

            // Wywołanie metody Post
            var result = await _controller.Post(request);

            // Sprawdzenie, czy wynik to OkObjectResult
            Assert.IsInstanceOf<OkObjectResult>(result);

        }
    }
}