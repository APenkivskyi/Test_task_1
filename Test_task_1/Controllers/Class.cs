/*using System;
using Microsoft.AspNetCore.Mvc;
using Test_task_1.Services;
using Test_task_1.Models;
using MongoDB.Driver;
using System.Security.Cryptography.X509Certificates;

namespace Test_task_1.Controllers;


[Controller]
[Route("api/[controller]")]
public class CustomerProductsController : Controller
{
    private readonly ICustomerRepository _mongoDBService;
    public CustomerProductsController(ICustomerRepository mongoDBService)
    {
        _mongoDBService = mongoDBService;
    }
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Request request)
    {
        try
        {
            string customerID = CreatingAclientAsync(request).Result;
            if (customerID != null)
            {
                OrderCreation(request, customerID);
                return Ok("OK");
            }
            return BadRequest();
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }
    public async Task<string> CreatingAclientAsync(Request request)
    {
        var customers = new Customers() // tworzenie obiektu kupującego
        {
            Customer_Name = request.Customer_Name,
            Customer_Delivery_Address = request.Customer_Delivery_Address,
            Customer_Surname = request.Customer_Surname
        };
        var existingCustomer = await _mongoDBService.FindCustomerAsync(request.Customer_Name, request.Customer_Surname, request.Customer_Delivery_Address);
        if (existingCustomer == null) // sprawdzamy czy zapisaliśmy ID
        {
            await _mongoDBService.CreateAsync(customers); // Tworzenie nowego rekordu
            return customers.Customer_Id;
        }
        else
        {
            return existingCustomer.Customer_Id;
        }
    }
    public async void OrderCreation(Request request, string customerID)
    {
        var orders = new Orders() // tworzymy zamówienie 
        {
            Order_Name = request.Order_Name,
            Order_Description = request.Order_Description,
            Order_Price = request.Order_Price,
            Order_CustomerId = customerID,
        };
        await _mongoDBService.CreateAsync(orders);
    }
    [HttpPost("{Customer_Name}/{Customer_Surname}/{Customer_Delivery_Address}/{Order_Name}/{Order_Description}/{Order_Price}")]
    public async Task<IActionResult> Post(string Customer_Name, string Customer_Surname, string Customer_Delivery_Address, string Order_Name, string Order_Description, int Order_Price)
    {
        try
        {
            var customers = new Customers() // tworzenie kupującego
            {
                Customer_Name = Customer_Name,
                Customer_Delivery_Address = Customer_Delivery_Address,
                Customer_Surname = Customer_Surname
            };
            var existingCustomer = await _mongoDBService.FindCustomerAsync(Customer_Name, Customer_Surname, Customer_Delivery_Address);
            var orders = new Orders() // tworzymy zamówienie 
            {
                Order_Name = Order_Name,
                Order_Description = Order_Description,
                Order_Price = Order_Price,
            };
            if (existingCustomer == null) // sprawdzamy czy zapisaliśmy ID
            {
                await _mongoDBService.CreateAsync(customers); // Tworzenie nowego rekordu
                orders.Order_CustomerId = customers.Customer_Id;
            }
            else
            {
                orders.Order_CustomerId = existingCustomer.Customer_Id;
            }
            await _mongoDBService.CreateAsync(orders); // Tworzenie nowego rekordu
            return Ok("OK");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }
}*/
