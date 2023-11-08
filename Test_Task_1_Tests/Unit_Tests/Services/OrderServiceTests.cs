/*using Castle.Core.Resource;
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
        private OrderService _orderService;
        [SetUp]
        public void Setup()
        {
            _customerRepository = new Mock<ICustomerRepository>();
            _orderService = new OrderService(_customerRepository.Object);
        }
        Request request = new Request
        {
            CustomerName = "Adam",
            CustomerSurname = "Kowalski",
            CustomerDeliveryAddress = "Beach street",
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
        public async Task CreatedNewOrder_ShouldCreateOrder()
        {
            string customerID = "3242354543232"; // Fake Id kupującego
            _customerRepository.Setup(x => x.FindCustomerId(customerID)).ReturnsAsync(customers);
            // Wywołanie metody OrderCreation
            await _orderService.OrderCreation(request, customerID);
            // Sprawdzenie, czy metoda OrderCreation dla zamówienia została wywołana
            _customerRepository.Verify(x => x.CreateAsync(It.IsAny<Orders>()), Times.Once);
        }

    }
}
*/