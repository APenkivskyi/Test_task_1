using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TestTask1.Services;
using TestTask1.Interface;
using TestTask1.Models;
using System.Dynamic;

namespace TestTask1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerProductsController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IOrderService _orderService;
        public CustomerProductsController(ICustomerService customerService, IOrderService orderService)
        {
            _customerService = customerService;
            _orderService = orderService;
        }
        [HttpPost("AddCustomer")]
        public async Task<IActionResult> AddCustomer([FromBody] Request request)
        {
            try
            {
                if (request != null)
                {
                    string customerId = await _customerService.CreatingClientAsync(request);
                    return Ok(customerId);
                }
                return BadRequest(string.Empty);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        [HttpPost("AddOrder")]
        public async Task<IActionResult> AddOrder([FromBody] Request request)
        {
            try
            {
                if (request != null)
                {
                    string orderId = await _orderService.OrderCreation(request, request.CustomerId);
                    if (orderId != null)
                    {
                        return Ok(orderId);
                    }
                    return BadRequest("Id customer not found");
                }
                return BadRequest(string.Empty);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        [HttpGet("SearchСustomerAndOrders")] // Wyszukiwamy klienta po ID
        public async Task<IActionResult> SearchСustomerAndOrders([FromBody] Request request)
        {
            try
            {
                if (request != null)
                {
                    var resultCustomer = await _customerService.FindCustomer(request); // sprawdzamy czy mamy taki ID w bazie danych
                    if (resultCustomer != null) // jeżeli mamy sprawdzamy czy są zamówienia klienta
                    {
                        var resultOrders = await _orderService.FindOrders(resultCustomer.CustomerId);
                        if (resultOrders != null)
                        {
                            dynamic result = new ExpandoObject();
                            result.customer = resultCustomer;
                            if (resultOrders.Any())
                            {
                                result.Orders = resultOrders;
                            }
                            else
                            {
                                result.Orders = "Brak zamówień";
                            }
                            return Ok(result);
                        }
                        return BadRequest("Błąd w pobieraniu zamówień klienta.");
                    }
                    return BadRequest("Klienta nie znaleziono!");
                }
                return BadRequest("Brak danych");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
