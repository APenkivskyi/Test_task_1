﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using TestTask1.Services;
using TestTask1.Interface;
using TestTask1.Models;
using Castle.Core.Resource;
using TestTask1.Controllers;

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
        public async Task ShouldAddOrderAndCustomer() // Sprawdzamy dodawanie klienta i zamówienia do bazy
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
            _mockICustomerService.Setup(x => x.CreatingClientAsync(request)).ReturnsAsync("342235234543"); // Symuluemy zwracanie funcją CreatingClientAsync ID kupującego.

            // Wywołanie metody AddCustomerOrOrder
            var result = await _controller.AddCustomerOrOrder(request);

            // Sprawdzenie, czy wynik to OkObjectResult
            Assert.IsInstanceOf<OkObjectResult>(result);

        }
        [Test]
        public async Task ShouldAddCustomer() // Sprawdzamy dodawanie klienta
        {
            // Przygotowanie danych testowych
            Request request = new Request
            {
                CustomerName = "Adam",
                CustomerSurname = "Kowalski",
                CustomerDeliveryAddress = "Beach street"
            };
            _mockICustomerService.Setup(x => x.CreatingClientAsync(request)).ReturnsAsync("342235234543");
            // Wywołanie metody AddCustomer
            var result = await _controller.AddCustomer(request) as OkObjectResult;
            // Sprawdzamy czy wywołana funkcja zwraca id customera
            var response = result.Value as string;
            Assert.AreEqual("342235234543", response);
        }
        [Test]
        public async Task ShouldAddOrder() // Sprawdzamy dodawanie zamówienia
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
    }
}