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
    public class OrdersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IOrderService _orderService;
        public OrdersController(ICustomerService customerService, IOrderService orderService)
        {
            _customerService = customerService;
            _orderService = orderService;
        }
        [HttpPost("AddOrder")]
        public async Task<IActionResult> AddOrder([FromBody] Orders order)
        {
            try
            {
                if (order != null)
                {
                    string orderId = await _orderService.OrderCreation(order);
                    if (orderId != null)
                    {
                        string result = "Order ID: " + orderId;
                        return Ok(result);
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
        [HttpGet("SearchOrders")] // Wyszukiwamy orders po ID customer'a
        public async Task<IActionResult> SearchOrders([FromQuery] string customerId)
        {
            try
            {
                if (customerId != null)
                {
                    var result = await _orderService.FindOrders(customerId);
                    if (result == null)
                    {
                        return BadRequest("Id customera nie znaleziono.");
                    } else if (result.Count > 0)
                    {
                        return Ok(result);
                    }
                    return NoContent();
                }
                return BadRequest("Brak id customer'a");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
