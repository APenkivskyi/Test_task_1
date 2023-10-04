using Castle.Core.Resource;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_task_1.Models;
using Test_task_1.Services;

namespace Test_Task_1_Tests.Unit_Tests.Services
{
    public class OrderServiceTests
    {
        private Mock<ICustomerAndOrderRepository> _customerRepository;
        private OrderService _orderService;
        [SetUp]
        public void Setup()
        {
            _customerRepository = new Mock<ICustomerAndOrderRepository>();
            _orderService = new OrderService(_customerRepository.Object);
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
        public async Task CreatedNewOrder_ShouldCreateOrder()
        {
            string customerID = "3242354543232"; // Fake Id kupującego
            // Wywołanie metody OrderCreation
            _orderService.OrderCreation(request, customerID);
            // Sprawdzenie, czy metoda OrderCreation dla zamówienia została wywołana
            _customerRepository.Verify(x => x.CreateAsync(It.IsAny<Orders>()), Times.Once);
        }

    }
}
