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
using FakeItEasy;

namespace TestTask1Tests.Unit_Tests.Controller
{
    public class CustomerProductsControllerTests
    {
        private Mock<ICustomerService> _mockICustomerService;
        private CustomerController _customerController;

        [SetUp]
        public void Setup()
        {
            _mockICustomerService = new Mock<ICustomerService>();
            _customerController = new CustomerController(_mockICustomerService.Object);
            
        }
        Customers customer = new Customers
        {
            CustomerId= "654b627bbf682ba783c038bc",
            CustomerName = "Adam",
            CustomerSurname = "Kowalski",
            CustomerDeliveryAddress = "Beach street",
            CustomerEmail = "Adam.kowalski@gmail.com"
        };
        [Test]
        public async Task AddCustomerResultSuccess() // Sprawdzamy dodawanie klienta
        {
            // Przygotowanie danych testowych
            _mockICustomerService.Setup(x => x.CreatingClientAsync(customer)).ReturnsAsync("654b627bbf682ba783c038bc");
            // Wywołanie metody AddCustomer
            var result = await _customerController.AddCustomer(customer) as OkObjectResult;
            // Sprawdzamy czy wywołana funkcja zwraca id customera
            var response = result.Value as string;
            Assert.AreEqual("654b627bbf682ba783c038bc", response);
        }
        [Test]
        public async Task SearchForCustomerUsingCustomerIdResultSuccess()
        {
            // Przygotowanie danych testowych
            Customers customerTests = new Customers
            {
                CustomerId = "654b627bbf682ba783c038bc"
            };
            string customerId= customerTests.CustomerId;
            _mockICustomerService.Setup(x => x.FindCustomer(It.Is<Customers>(c => c.CustomerId == customerTests.CustomerId)))
                    .ReturnsAsync(customer);
            // Wywołanie metody 
            var result = await _customerController.SearchСustomer(customerId) as OkObjectResult;
            // Sprawdzamy czy wywołana funkcja zwraca customera
            Assert.NotNull(result);
            Assert.IsInstanceOf<Customers>(result.Value);
            Assert.AreEqual(customer, result.Value);
        }
    }
}