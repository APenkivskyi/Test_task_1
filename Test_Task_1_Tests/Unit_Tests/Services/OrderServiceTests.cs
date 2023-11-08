using Castle.Core.Resource;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask1.Interface;
using TestTask1.Models;
using TestTask1.Services;

namespace TestTask1Tests.Unit_Tests.Services
{
    public class OrderServiceTests
    {
        private Mock<ICustomerRepository> _customerRepository;
        private Mock<IOrderRepository> _oderRepository;
        private OrderService _orderService;
        [SetUp]
        public void Setup()
        {
            _customerRepository = new Mock<ICustomerRepository>();
            _oderRepository = new Mock<IOrderRepository>();
            _orderService = new OrderService(_oderRepository.Object, _customerRepository.Object);
        }
        Orders order = new Orders
        {
            OrderCustomerId = "32423543563454",
            OrderName = "Sample Order",
            OrderDescription = "Sample Description",
            OrderPrice = 100
        };
        Customers customers = new Customers
        {
            CustomerName = "Adam",
            CustomerSurname = "Kowalski",
            CustomerDeliveryAddress = "Beach street",
            CustomerId = "32423543563454"
        };
        [Test]
        public async Task CreateNewOrder_ResultCreatedNewOrder()
        {
            _customerRepository.Setup(x => x.FindCustomerId(customers.CustomerId)).ReturnsAsync(customers);
            // Wywołanie metody OrderCreation
            await _orderService.OrderCreation(order);
            // Sprawdzenie, czy metoda OrderCreation dla zamówienia została wywołana
            _oderRepository.Verify(x => x.CreateAsync(It.IsAny<Orders>()), Times.Once);
        }
        [Test]
        public async Task OrderSearchUsingCustomerIdResultOrder()
        {
            // przygotowanie danych
            var ordersList = new List<Orders> { order };
            _customerRepository.Setup(x => x.FindCustomerId(order.OrderCustomerId)).ReturnsAsync(customers);
            _oderRepository.Setup(x => x.FindOrdersByCustomerId(customers.CustomerId)).ReturnsAsync(ordersList);
            // Wywołanie funkcji 
            var result = _orderService.FindOrders(customers.CustomerId);
            // Sprawdzamy wynik
            Assert.AreEqual(result.Result.Count, 1);
            Assert.AreEqual(result.Result[0], ordersList[0]);
        }

}
}
