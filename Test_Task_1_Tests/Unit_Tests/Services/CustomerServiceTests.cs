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
        private Mock<ICustomerRepository> _customerRepository;
        private CustomerService _customerService;
        [SetUp]
        public void Setup()
        {
            _customerRepository = new Mock<ICustomerRepository>();
            _customerService = new CustomerService(_customerRepository.Object);
        }
        Customers customer = new Customers
        {
            CustomerName = "Adam",
            CustomerSurname = "Kowalski",
            CustomerDeliveryAddress = "Beach street",
            CustomerId = "32423543563454",
            CustomerEmail = "Adam.kowalski@gmail.com"
        };
        [Test]
        public async Task CreateNewCustomer_ShouldReturnNullIdCustomer()
        {
            _customerRepository.Setup(x => x.FindCustomer(customer.CustomerEmail))
                .ReturnsAsync((Customers)null);
            // Wywołanie metody CreatingClientAsync
            var result = await _customerService.CreatingClientAsync(customer);
            // Sprawdzenie, czy metoda Create dla klienta została wywołana
            _customerRepository.Verify(x => x.Create(It.IsAny<Customers>()), Times.Once);
        }
        [Test]
        public async Task CreatingAnExistingCustomer_ShouldReturnIdCustomer()
        {
            _customerRepository.Setup(x => x.FindCustomer(customer.CustomerEmail))
                .ReturnsAsync(customer);
            // Wywołanie metody CreatingClientAsync
            var result = await _customerService.CreatingClientAsync(customer);
            // Sprawdzenie, czy wynik to Id kupującego
            Assert.AreEqual(customer.CustomerId, result);
            // Sprawdzenie, czy metoda Create dla klienta została wywołana
            _customerRepository.Verify(x => x.Create(It.IsAny<Customers>()), Times.Never);
        }
        [Test]
        public async Task CustomerSearchUsingIdResultSuccessful()
        {
            // Przygotowanie danych testowych
            _customerRepository.Setup(x => x.FindCustomerId(customer.CustomerId))
                .ReturnsAsync(customer);
            Customers testCustomer = new Customers
            {
                CustomerId = customer.CustomerId,
            };
            // Wywołanie metody
            var result = _customerService.FindCustomer(testCustomer);
            // Sprawdzamy wynik
            Assert.AreEqual(customer.CustomerName, result.Result.CustomerName);
            Assert.AreEqual(customer.CustomerSurname, result.Result.CustomerSurname);
            Assert.AreEqual(customer.CustomerId, result.Result.CustomerId);
            Assert.AreEqual(customer.CustomerEmail, result.Result.CustomerEmail);
            Assert.AreEqual(customer.CustomerDeliveryAddress, result.Result.CustomerDeliveryAddress);
            _customerRepository.Verify(x => x.FindCustomerId(customer.CustomerId), Times.Once);
            _customerRepository.Verify(x => x.FindCustomer(customer.CustomerEmail), Times.Never);
        }
        [Test]
        public async Task CustomerSearchUsingEmailResultSuccessful()
        {
            // Przygotowanie danych testowych
            _customerRepository.Setup(x => x.FindCustomer(customer.CustomerEmail))
                .ReturnsAsync(customer);
            Customers testCustomer = new Customers
            {
                CustomerEmail = customer.CustomerEmail,
            };
            // Wywołanie metody
            var result = _customerService.FindCustomer(testCustomer);
            // Sprawdzamy wynik
            Assert.AreEqual(customer.CustomerName, result.Result.CustomerName);
            Assert.AreEqual(customer.CustomerSurname, result.Result.CustomerSurname);
            Assert.AreEqual(customer.CustomerId, result.Result.CustomerId);
            Assert.AreEqual(customer.CustomerEmail, result.Result.CustomerEmail);
            Assert.AreEqual(customer.CustomerDeliveryAddress, result.Result.CustomerDeliveryAddress);
            _customerRepository.Verify(x => x.FindCustomerId(customer.CustomerId), Times.Never);
            _customerRepository.Verify(x => x.FindCustomer(customer.CustomerEmail), Times.Once);
        }
    }
}