using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask1.Controllers;
using TestTask1.Interface;
using TestTask1.Models;
using TestTask1.Services;

namespace TestTask1Tests.Unit_Tests.Services
{
    public class CustomerServiceTests
    {
        private Mock<ICustomerAndOrderRepository> _customerRepository;
        private CustomerService _customerService;
        [SetUp]
        public void Setup()
        {
            _customerRepository = new Mock<ICustomerAndOrderRepository>();
            _customerService = new CustomerService(_customerRepository.Object);
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
        [Test]
        public async Task CreateNewCustomer_ShouldReturnNullIdCustomer()
        {
            _customerRepository.Setup(x => x.FindCustomerAsync(request.CustomerName, request.CustomerSurname, request.CustomerDeliveryAddress))
                .ReturnsAsync((Customers)null);
            // Wywołanie metody CreatingClientAsync
            var result = await _customerService.CreatingClientAsync(request);
            // Sprawdzenie, czy wynik to Id kupującego
            Assert.IsNull(result);
            // Sprawdzenie, czy metoda CreateAsync dla klienta została wywołana
            _customerRepository.Verify(x => x.CreateAsync(It.IsAny<Customers>()), Times.Once);
        }
        [Test]
        public async Task CreatingAnExistingCustomer_ShouldReturnIdCustomer()
        {
            Customers customers = new Customers
            {
                CustomerName = "Adam",
                CustomerSurname = "Kowalski",
                CustomerDeliveryAddress = "Beach street",
                CustomerId = "32423543563454"
            };
            _customerRepository.Setup(x => x.FindCustomerAsync(request.CustomerName, request.CustomerSurname, request.CustomerDeliveryAddress))
                .ReturnsAsync(customers);
            // Wywołanie metody CreatingClientAsync
            var result = await _customerService.CreatingClientAsync(request);
            // Sprawdzenie, czy wynik to Id kupującego
            Assert.AreEqual(customers.CustomerId, result);
            // Sprawdzenie, czy metoda CreateAsync dla klienta została wywołana
            _customerRepository.Verify(x => x.CreateAsync(It.IsAny<Customers>()), Times.Never);
        }
    }
}
