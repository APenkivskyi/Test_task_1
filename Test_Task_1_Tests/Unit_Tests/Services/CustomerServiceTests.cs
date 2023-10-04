using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_task_1.Controllers;
using Test_task_1.Interface;
using Test_task_1.Models;
using Test_task_1.Services;

namespace Test_Task_1_Tests.Unit_Tests.Services
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
            Customer_Name = "Adam",
            Customer_Surname = "Kowalski",
            Customer_Delivery_Address = "Beach street",
            Order_Name = "Sample Order",
            Order_Description = "Sample Description",
            Order_Price = 100
        };
        [Test]
        public async Task CreateNewCustomer_ShouldReturnNullIdCustomer()
        {
            _customerRepository.Setup(x => x.FindCustomerAsync(request.Customer_Name, request.Customer_Surname, request.Customer_Delivery_Address))
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
                Customer_Name = "Adam",
                Customer_Surname = "Kowalski",
                Customer_Delivery_Address = "Beach street",
                Customer_Id="32423543563454"
            };
            _customerRepository.Setup(x => x.FindCustomerAsync(request.Customer_Name, request.Customer_Surname, request.Customer_Delivery_Address))
                .ReturnsAsync((Customers)customers);
            // Wywołanie metody CreatingClientAsync
            var result = await _customerService.CreatingClientAsync(request);
            // Sprawdzenie, czy wynik to Id kupującego
            Assert.AreEqual(customers.Customer_Id, result);
            // Sprawdzenie, czy metoda CreateAsync dla klienta została wywołana
            _customerRepository.Verify(x => x.CreateAsync(It.IsAny<Customers>()), Times.Never);
        }
    }
}
