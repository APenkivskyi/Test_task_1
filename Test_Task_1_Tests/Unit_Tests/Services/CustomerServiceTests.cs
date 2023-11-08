/*using Microsoft.AspNetCore.Mvc;
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
        private Mock<ICustomerRepository> _customerRepository;
        private CustomerService _customerService;
        [SetUp]
        public void Setup()
        {
            _customerRepository = new Mock<ICustomerRepository>();
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
        Customers customer = new Customers
        {
            CustomerName = "Adam",
            CustomerSurname = "Kowalski",
            CustomerDeliveryAddress = "Beach street",
            CustomerId = "32423543563454",
        };
        [Test]
        public async Task CreateNewCustomer_ShouldReturnNullIdCustomer()
        {
            _customerRepository.Setup(x => x.FindCustomerAsync(request.CustomerName, request.CustomerSurname, request.CustomerDeliveryAddress))
                .ReturnsAsync((Customers)null);
            // Wywołanie metody CreatingClientAsync
            var result = await _customerService.CreatingClientAsync(customer);
            // Sprawdzenie, czy metoda Create dla klienta została wywołana
            _customerRepository.Verify(x => x.Create(It.IsAny<Customers>()), Times.Once);
        }
        [Test]
        public async Task CreatingAnExistingCustomer_ShouldReturnIdCustomer()
        {
            _customerRepository.Setup(x => x.FindCustomerAsync(request.CustomerName, request.CustomerSurname, request.CustomerDeliveryAddress))
                .ReturnsAsync(customer);
            // Wywołanie metody CreatingClientAsync
            var result = await _customerService.CreatingClientAsync(customer);
            // Sprawdzenie, czy wynik to Id kupującego
            Assert.AreEqual(customer.CustomerId, result);
            // Sprawdzenie, czy metoda Create dla klienta została wywołana
            _customerRepository.Verify(x => x.Create(It.IsAny<Customers>()), Times.Never);
        }
    }
}
*/