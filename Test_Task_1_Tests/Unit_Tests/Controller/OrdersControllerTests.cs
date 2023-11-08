using System;
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
    public class OrdersControllerTests
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
        public async Task ShouldAddOrderResultSuccess() // Sprawdzamy dodawanie zamówienia
        {
            // Przygotowanie danych testowych
            Orders order = new Orders
            {
                OrderCustomerId = "2342356436453",
                OrderName = "Sample Order",
                OrderDescription = "Sample Description",
                OrderPrice = 100
            };
            _mockIOrderService.Setup(x => x.OrderCreation(order)).ReturnsAsync("2342356436453");
            // Wywołanie metody AddOrder
            var result = await _controller.AddOrder(order) as OkObjectResult;
            // Sprawdzamy czy wywołana funkcja zwraca id zamówienia oraz kod 200.
            var response = result.Value as string;
            Assert.AreEqual("Order ID: 2342356436453", response);
            Assert.AreEqual(200, result.StatusCode);
        }
        [Test]
        public async Task SearchOrdersByCustomerIdResultSuccess()
        {
            // Arrange
            string customerId = "2342356436453";
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
            _mockIOrderService.Setup(x => x.FindOrders(customer.CustomerId)).ReturnsAsync(order);
            // Act
            var result = await _controller.SearchOrders(customerId) as OkObjectResult;
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            var resultOrders = result.Value as List<Orders>;
            Assert.IsNotNull(resultOrders);
            Assert.AreEqual(order.Count, resultOrders.Count);
            foreach (var expectedOrder in order)
            {
                var resultOrder = resultOrders.FirstOrDefault(o => o.OrderId == expectedOrder.OrderId);
                Assert.IsNotNull(resultOrder);
                Assert.AreEqual(expectedOrder.OrderName, resultOrder.OrderName);
                Assert.AreEqual(expectedOrder.OrderDescription, resultOrder.OrderDescription);
                Assert.AreEqual(expectedOrder.OrderPrice, resultOrder.OrderPrice);
            }
        }
    }
}