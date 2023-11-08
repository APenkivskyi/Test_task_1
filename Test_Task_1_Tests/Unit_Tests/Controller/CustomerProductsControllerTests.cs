/*using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using TestTask1.Services;
using TestTask1.Interface;
using TestTask1.Models;
using Castle.Core.Resource;
using TestTask1.Controllers;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Dynamic;

namespace TestTask1Tests.Unit_Tests.Controller
{
    public class CustomerProductsControllerTests
    {
        private Mock<ICustomerService> _mockICustomerService;
        private Mock<IOrderService> _mockIOrderService;
        private OrdersController _controller;

        [SetUp]
        public void Setup()
        {
            _mockICustomerService = new Mock<ICustomerService>();
            _mockIOrderService = new Mock<IOrderService>();
            _controller = new OrdersController(_mockICustomerService.Object, _mockIOrderService.Object);
        }
        [Test]
        public async Task ShouldAddCustomerResultSuccess() // Sprawdzamy dodawanie klienta
        {
            // Przygotowanie danych testowych
            Customers customer = new Customers
            {
                CustomerName = "Adam",
                CustomerSurname = "Kowalski",
                CustomerDeliveryAddress = "Beach street"
            };
            _mockICustomerService.Setup(x => x.CreatingClientAsync(customer)).ReturnsAsync("342235234543");
            // Wywołanie metody AddCustomer
            var result = await _controller.AddCustomer(customer) as OkObjectResult;
            // Sprawdzamy czy wywołana funkcja zwraca id customera
            var response = result.Value as string;
            Assert.AreEqual("342235234543", response);
        }
        [Test]
        public async Task ShouldAddOrderResultSuccess() // Sprawdzamy dodawanie zamówienia
        {
            // Przygotowanie danych testowych
            Request request = new Request
            {
                CustomerId = "2342356436453",
                OrderName = "Sample Order",
                OrderDescription = "Sample Description",
                OrderPrice = 100
            };
            _mockIOrderService.Setup(x => x.OrderCreation(request, request.CustomerId)).ReturnsAsync("23432523345");
            // Wywołanie metody AddOrder
            var result = await _controller.AddOrder(request) as OkObjectResult;
            // Sprawdzamy czy wywołana funkcja zwraca id zamówienia oraz kod 200.
            var response = result.Value as string;
            Assert.AreEqual("23432523345", response);
            Assert.AreEqual(200, result.StatusCode);
        }
        [Test]
        public async Task SearchOrdersByCustomerIdResultSuccess()
        {
            // Arrange
            Request request = new Request
            {
                CustomerId = "2342356436453"
            };
            Customers customer = new Customers
            {
                CustomerId = "2342356436453",
                CustomerName = "Jan",
                CustomerSurname = "Kowalski"
            };
            List<Orders> order = new List<Orders>
    {
        new Orders
        {
            OrderId = "4234532523246546435",
            OrderCustomerId = "2342356436453",
            OrderName = "Koszulka",
            OrderDescription = "Koszulka BOSS",
            OrderPrice = 150
        },
        new Orders
        {
            OrderId = "423453252324435",
            OrderCustomerId = "2342356436453",
            OrderName = "Spodnie",
            OrderDescription = "Najlepsze spodnie",
            OrderPrice = 200
        }
    };
            _mockICustomerService.Setup(x => x.FindCustomer(customer)).ReturnsAsync(customer);
            _mockIOrderService.Setup(x => x.FindOrders(customer.CustomerId)).ReturnsAsync(order);

            // Act
            var result = await _controller.SearchСustomerAndOrders(request) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            var response = result.Value as ExpandoObject;
            Assert.IsNotNull(response);
            dynamic resultData = response as dynamic;
            Assert.IsNotNull(resultData.customer);
            var resultCustomer = resultData.customer as Customers;
            Assert.AreEqual(customer.CustomerId, resultCustomer.CustomerId);
            Assert.AreEqual(customer.CustomerName, resultCustomer.CustomerName);
            Assert.AreEqual(customer.CustomerSurname, resultCustomer.CustomerSurname);
            Assert.IsNotNull(resultData.Orders);
            var resultOrders = resultData.Orders as List<Orders>;
            Assert.AreEqual(order.Count, resultOrders.Count);
        }

    }
}*/