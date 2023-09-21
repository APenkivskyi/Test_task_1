using System;
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
    private readonly MongoDBService _mongoDBService;
    public CustomerProductsController(MongoDBService mongoDBService)
    {
        _mongoDBService = mongoDBService;
    }
    [HttpGet]
    public async Task<List<Customers>> Get() {
        return await _mongoDBService.GetAsync();
    }

    /*[HttpPost]
    public async Task<IActionResult> Post([FromBody] Customers customers)
    {
        if (customers.Customer_Name != null && customers.Customer_Surname != null && customers.Customer_Delivery_Address != null)
        {
            var existingCustomer = await _mongoDBService._customersCollection
                .Find(x => x.Customer_Name == customers.Customer_Name && x.Customer_Surname == customers.Customer_Surname && x.Customer_Delivery_Address == customers.Customer_Delivery_Address)
                .FirstOrDefaultAsync();

            if (existingCustomer == null)
            {
                await _mongoDBService.CreateAsync(customers); // Tworzenie nowego rekordu
                _mongoDBService.HistoryID = customers.Customer_Id;
                return CreatedAtAction(nameof(Get), new { id = customers.Customer_Id }, customers);
            }
            else
            {
                // Rekord o takich samych danych już istnieje
                return BadRequest("Rekord o takich samych danych kupującego już istnieje.");
            }
        }
        else
        {
            // Pola Customer_Name, Customer_Surname i Customer_Delivery_Address nie mogą być NULL
            return BadRequest("Pola Customer_Name, Customer_Surname i Customer_Delivery_Address nie mogą być puste.");
        }
    }*/
    [HttpPost("{Customer_Name}/{Customer_Surname}/{Customer_Delivery_Address}/{Order_Name}/{Order_Description}/{Order_Price}")]
    public async Task<IActionResult> Post(string Customer_Name, string Customer_Surname, string Customer_Delivery_Address, string Order_Name, string Order_Description, int Order_Price)
    {
        try
        {
            string HistoryID = null; // zmienna na wypadek tworzenia kupującego 
            if(Customer_Name != null && Customer_Surname != null && Customer_Delivery_Address != null && Order_Name != null && Order_Price != null && Order_Description != null) // sprawdzamy czy nie puste są pola
            {
                var customers = new Customers() // tworzenie kupującego
                {
                    Customer_Name = Customer_Name,
                    Customer_Delivery_Address = Customer_Delivery_Address,
                    Customer_Surname = Customer_Surname
                };
                var existingCustomer = await _mongoDBService._customersCollection
                .Find(x => x.Customer_Name == customers.Customer_Name && x.Customer_Surname == customers.Customer_Surname && x.Customer_Delivery_Address == customers.Customer_Delivery_Address)
                .FirstOrDefaultAsync(); // sprawdzamy w baze czy mamy klienta o takich wartościach jak imię nazwisko i adres dostarczenia
                if (existingCustomer == null) //jeżeli brak to tworzymy, jak nie pomijamy 
                {
                    await _mongoDBService.CreateAsync(customers); // Tworzenie nowego rekordu
                    HistoryID = customers.Customer_Id;
                }
                var orders = new Orders() // tworzymy zamówienie 
                {
                    Order_Name = Order_Name,
                    Order_Description = Order_Description,
                    Order_Price = Order_Price,
                };
                if(HistoryID != null) // sprawdzamy czy zapisaliśmy ID
                {
                    orders.Order_CustomerId = HistoryID;
                }
                else
                {
                    orders.Order_CustomerId = existingCustomer.Customer_Id;
                }
                await _mongoDBService.CreateAsync(orders); // Tworzenie nowego rekordu
            }
            else
            {
                // Pola nie mogą być NULL
                return BadRequest("Pola nie mogą być puste.");
            }
            return Ok("OK");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }
    /*public async Task<IActionResult> Post([FromBody] Orders orders)
    {
        if (orders.Order_Name != null && orders.Order_Description != null && orders.Order_Price != null)
        {
            orders.Order_Id = _mongoDBService.HistoryID;
            await _mongoDBService.CreateAsync(orders); // Tworzenie nowego rekordu
            return CreatedAtAction(nameof(Get), new { id = orders.Order_Id }, orders);
        }
        else
        {
            // Pola Customer_Name, Customer_Surname i Customer_Delivery_Address nie mogą być NULL
            return BadRequest("Pola Customer_Name, Customer_Surname i Customer_Delivery_Address nie mogą być puste.");
        }
    }*/




    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id) {
        await _mongoDBService.DeleteAsync(id);
        return NoContent();
    }

}
